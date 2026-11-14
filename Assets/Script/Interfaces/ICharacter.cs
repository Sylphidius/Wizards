using UnityEngine;

public interface ICharacter
{
    Team GetTeam();
    void TakeDamage(int result);
    void Heal(int result);
    void AddPressure(int result);
    void ChangeStats(Statistics stat);
    void RemovePressure(int result);
    void InitFightStats();
    FightManagerBehaviour FightManager { get; }
    Statistics FightStats { get; }
}

public enum Team 
{
    Ally,
    Enemy,
    Player
}
