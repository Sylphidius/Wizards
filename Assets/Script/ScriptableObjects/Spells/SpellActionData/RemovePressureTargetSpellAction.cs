using UnityEngine;

[CreateAssetMenu(fileName = "RemovePressureTargetSpellAction", menuName = "Scriptable Objects/SpellActionData/RemovePressureTargetSpellAction")]
public class RemovePressureTargetSpellAction : SpellActionData
{
    public int potency;
    public override void Act(ICharacter user, ICharacter target)
    {
        int result = potency;
        Debug.Log("Damage Target for " + result);

        target.RemovePressure(result);
    }

}
