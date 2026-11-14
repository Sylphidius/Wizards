using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour, ICharacter
{
    [SerializeField]
    protected Team team;
    [SerializeField]
    protected Statistics baseStats;
    [SerializeField] 
    protected Statistics fightStats;
    [SerializeField]
    protected List<SpellData> spellbook;
    [SerializeField]
    protected List<SpellData> activeSpells;
    [SerializeField]
    protected CharacterData characterData;
    protected int currentHp;

    public Statistics FightStats => fightStats;

    public Team GetTeam()
    {
        return team;
    }

    public Team SetTeam()
    {
        return team;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseStats = new Statistics();
        spellbook = new List<SpellData>();
        activeSpells = new List<SpellData>();
        LoadCharacterData();
        if (spellbook.Count > 0)
            spellbook[0].ActivateSpell(this, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadCharacterData()
    {
        LoadCharacterData(characterData);
    }
    public void LoadCharacterData(CharacterData data)
    {
        baseStats.InitWithCharacterData(data);
        spellbook.Clear();
        activeSpells.Clear();
        spellbook.AddRange(data.spellbook.ToArray());
        activeSpells.AddRange(data.activeSpells.ToArray());
        currentHp = data.hp;
    }

    public void TakeDamage(int result)
    {
        currentHp-= result;
        NotifyDamage();
    }


    protected virtual void NotifyDamage()
    {
        Debug.Log("DamageTaken");
    }

    public void Heal(int result)
    {
        throw new NotImplementedException();
    }

    public void AddPressure(int result)
    {
        throw new NotImplementedException();
    }

    public void ChangeStats(Statistics stat)
    {
        throw new NotImplementedException();
    }

    public void RemovePressure(int result)
    {
        throw new NotImplementedException();
    }
}

[System.Serializable]
public struct Statistics
{
    public int power;
    public int will;
    public int defense;
    public int speed;
    public int hp;
    public int pressure;

    public Statistics(int a)
    {
        power = a;
        will = a;
        defense = a;
        speed = a;
        hp = a;
        pressure = a;
    }
    public void Initialize()
    {
        power = 0;
        will = 0;
        defense = 0;
        speed = 0;
        hp = 0;
        pressure = 0;
    }

    internal void InitWithCharacterData(CharacterData data)
    {
        power = data.power;
        will = data.will;
        defense = data.defense;
        speed = data.speed;
        hp = data.hp;
        pressure = data.pressure;
    }

}
