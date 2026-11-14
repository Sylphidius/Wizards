using UnityEngine;

[CreateAssetMenu(fileName = "AddPressureTargetSpellAction", menuName = "Scriptable Objects/SpellActionData/AddPressureTargetSpellAction")]
public class AddPressureTargetSpellAction : SpellActionData
{
    public int potency;
    public override void Act(ICharacter user, ICharacter target)
    {
        int result = potency;
        Debug.Log("Damage Target for " + result);

        target.AddPressure(result);
    }

}
