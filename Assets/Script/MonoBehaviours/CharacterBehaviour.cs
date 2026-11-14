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
    protected int currentPressure;


    private FightManagerBehaviour fightManager;

    public Statistics FightStats => fightStats;

    // Récupère (et met en cache) le FightManager présent dans la scène
    public FightManagerBehaviour FightManager
    {
        get
        {
            if (fightManager == null)
                fightManager = FindAnyObjectByType<FightManagerBehaviour>();
            return fightManager;
        }
    }

    // Appelé par le FightManager au lancement du combat
    public void InitFightStats()
    {
        fightStats = baseStats;
    }

    public Team GetTeam()
    {
        return team;
    }

    public void SetTeam(Team newTeam)
    {
        team = newTeam;
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
        currentPressure = 0;
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
        // Les PV ne peuvent pas dépasser le maximum du combat en cours
        currentHp = Mathf.Min(currentHp + result, fightStats.hp);
        Debug.Log("Healed for " + result + ", HP: " + currentHp);
    }

    public void AddPressure(int result)
    {
        currentPressure = Mathf.Min(currentPressure + result, fightStats.pressure);
        Debug.Log("Pressure added: " + result + ", Pressure: " + currentPressure);
    }

    // Applique les valeurs de stat comme des bonus/malus ajoutés aux stats de combat
    public void ChangeStats(Statistics stat)
    {
        int previousMaxHp = fightStats.hp;

        fightStats += stat;
        ClampFightStats();

        // currentHp augmente d'autant que le maximum augmente
        int hpGain = fightStats.hp - previousMaxHp;
        if (hpGain > 0)
            currentHp += hpGain;

        // currentHp ne peut pas dépasser le nouveau maximum
        currentHp = Mathf.Min(currentHp, fightStats.hp);

        // La pression ne peut pas dépasser son nouveau maximum
        currentPressure = Mathf.Min(currentPressure, fightStats.pressure);
    }

    // Toutes les stats sont >= 0, sauf hp qui est >= 1
    private void ClampFightStats()
    {
        fightStats.power = Mathf.Max(fightStats.power, 0);
        fightStats.will = Mathf.Max(fightStats.will, 0);
        fightStats.defense = Mathf.Max(fightStats.defense, 0);
        fightStats.speed = Mathf.Max(fightStats.speed, 0);
        fightStats.pressure = Mathf.Max(fightStats.pressure, 0);
        fightStats.hp = Mathf.Max(fightStats.hp, 1);
    }

    public void RemovePressure(int result)
    {
        currentPressure = Mathf.Max(currentPressure - result, 0);
        Debug.Log("Pressure removed: " + result + ", Pressure: " + currentPressure);
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

    public static Statistics operator +(Statistics a, Statistics b)
    {
        return new Statistics
        {
            power = a.power + b.power,
            will = a.will + b.will,
            defense = a.defense + b.defense,
            speed = a.speed + b.speed,
            hp = a.hp + b.hp,
            pressure = a.pressure + b.pressure
        };
    }

    public static Statistics operator -(Statistics a, Statistics b)
    {
        return new Statistics
        {
            power = a.power - b.power,
            will = a.will - b.will,
            defense = a.defense - b.defense,
            speed = a.speed - b.speed,
            hp = a.hp - b.hp,
            pressure = a.pressure - b.pressure
        };
    }

}
