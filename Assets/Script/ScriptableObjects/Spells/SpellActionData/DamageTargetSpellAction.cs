using UnityEngine;

[CreateAssetMenu(fileName = "DamageTargetSpellAction", menuName = "Scriptable Objects/SpellActionData/DamageTargetSpellAction")]
public class DamageTargetSpellAction : SpellActionData
{
    public int potency;
    protected override void SpellAction(ICharacter user, ICharacter target)
    {
        int result = Mathf.CeilToInt((1 + (user.FightStats.power - target.FightStats.defense) * 0.01f) * potency);
        Debug.Log("Damage Target for " + result);

        target.TakeDamage(result);
    }

}
