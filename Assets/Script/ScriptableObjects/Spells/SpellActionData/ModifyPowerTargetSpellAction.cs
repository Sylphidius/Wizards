using UnityEngine;

[CreateAssetMenu(fileName = "ModifyPowerTargetSpellAction ", menuName = "Scriptable Objects/SpellActionData/UpPowerTargetSpellAction ")]
public class ModifyPowerTargetSpellAction: SpellActionData
{
    public int potency;
    public override void Act(ICharacter user, ICharacter target)
    {
        int result = potency;
        Debug.Log("Damage Target for " + result);
        Statistics stat = new();
        stat.Initialize();
        stat.power = result;
        target.ChangeStats(stat);
    }

}
