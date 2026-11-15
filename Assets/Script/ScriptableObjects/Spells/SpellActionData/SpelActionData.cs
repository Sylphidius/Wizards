using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellActionData : ScriptableObject
{
    public string actionName;

    [Header("Ciblage")]
    [Tooltip("Vise le lanceur du sort au lieu de la cible du sort. Ignoré si 'Use Team Target' est coché.")]
    public bool targetSelf;
    [Tooltip("Vise le premier personnage de 'Team Target' au lieu de la cible du sort. Prioritaire sur 'Target Self'.")]
    public bool useTeamTarget;
    public Team teamTarget;

    public string Description { get => GetDescripton(); }

    public virtual string GetDescripton()
    {
        throw new NotImplementedException();
    }

    // Point d'entrée appelé par les utilisateurs du sort :
    // redéfinit la cible si nécessaire, puis exécute l'effet de l'action
    public void Act(ICharacter user, ICharacter target)
    {
        ICharacter finalTarget = ResolveTarget(user, target);
        if (finalTarget == null)
        {
            Debug.LogWarning(name + ": no valid target found, action skipped");
            return;
        }

        SpellAction(user, finalTarget);
    }

    // Effet propre à chaque action (l'ancien contenu de Act)
    protected abstract void SpellAction(ICharacter user, ICharacter target);

    private ICharacter ResolveTarget(ICharacter user, ICharacter target)
    {
        // Team Target est toujours prioritaire sur Target Self
        if (useTeamTarget)
        {
            FightManagerBehaviour fightManager = user.FightManager;
            if (fightManager == null)
                return null;

            List<ICharacter> members = fightManager.GetCharacters(teamTarget);
            return members.Count > 0 ? members[0] : null;
        }

        if (targetSelf)
            return user;

        return target;
    }
}
