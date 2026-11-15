using UnityEngine;

[CreateAssetMenu(fileName = "RemovePressureTargetSpellAction", menuName = "Scriptable Objects/SpellActionData/RemovePressureTargetSpellAction")]
public class RemovePressureTargetSpellAction : SpellActionData
{
    public int potency;
    protected override void SpellAction(ICharacter user, ICharacter target)
    {
        int result = potency;
        Debug.Log("Damage Target for " + result);

        target.RemovePressure(result);
    }

}
