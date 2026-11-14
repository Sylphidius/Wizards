using UnityEngine;

[CreateAssetMenu(fileName = "HealTargetSpellAction", menuName = "Scriptable Objects/SpellActionData/HealTargetSpellAction")]
public class HealTargetSpellAction : SpellActionData
{
    public int potenty;
    public override void Act(ICharacter user, ICharacter target)
    {
        int result = Mathf.CeilToInt((1 + (user.FightStats.will) * 0.01f) * potenty);
        Debug.Log("Damage Target for " + result);

        target.Heal(result);
    }

}
