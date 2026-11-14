using UnityEngine;

[CreateAssetMenu(fileName = "RemovePressureTargetSpellAction", menuName = "Scriptable Objects/SpellActionData/RemovePressureTargetSpellAction")]
public class RemovePressureTargetSpellAction : SpellActionData
{
    public int potenty;
    public override void Act(ICharacter user, ICharacter target)
    {
        int result = potenty;
        Debug.Log("Damage Target for " + result);

        target.RemovePressure(result);
    }

}
