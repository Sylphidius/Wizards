using UnityEngine;

[CreateAssetMenu(fileName = "HealTargetSpellAction", menuName = "Scriptable Objects/SpellActionData/HealTargetSpellAction")]
public class HealTargetSpellAction : SpellActionData
{
    public int potency;
    public override void Act(ICharacter user, ICharacter target)
    {
        int result = Mathf.CeilToInt((1 + (user.FightStats.will) * 0.01f) * potency);
        Debug.Log("Damage Target for " + result);

        target.Heal(result);
    }

}
