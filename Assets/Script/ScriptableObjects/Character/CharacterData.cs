using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public int hp;
    public int pressure;
    public List<SpellData> spellbook;
    public List<SpellData> activeSpells;
    public int power;
    public int will;
    public int defense;
    public int speed;
}
