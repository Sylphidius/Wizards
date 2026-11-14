using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "Scriptable Objects/SpellData")]
public class SpellData : ScriptableObject
{
    public List<SpellActionData> actions;
    public List<Team> allowedTargets;
    public bool selfTarget;
    public void ActivateSpell(ICharacter user,ICharacter target)
    {
        actions.ForEach(x => x.Act(user, target));
    }
    public bool IsValidTarget(ICharacter user, ICharacter target)
    {
        return allowedTargets.Contains(target.GetTeam()) || selfTarget && target == user;
    }
}
