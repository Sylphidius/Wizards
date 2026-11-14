using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightManagerBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<ICharacter> characters = new List<ICharacter>();

    public IReadOnlyList<ICharacter> Characters => characters;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CollectCharacters();
        Debug.Log(characters.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Retourne les personnages appartenant à l'équipe donnée
    public List<ICharacter> GetCharacters(Team team)
    {
        return characters.Where(c => c.GetTeam() == team).ToList();
    }

    // Récupère tous les ICharacter actifs de la scène
    public void CollectCharacters()
    {
        characters.Clear();
        characters.AddRange(
            FindObjectsByType<MonoBehaviour>().OfType<ICharacter>());
    }
    // Ally et Player forment un seul camp, Enemy est l'autre
    public static bool AreAllies(Team a, Team b)
    {
        return (a == Team.Enemy) == (b == Team.Enemy);
    }

    // Retourne les personnages du même camp que le personnage donné (lui-même inclus)
    public List<ICharacter> GetAllies(ICharacter character)
    {
        return characters.Where(c => AreAllies(c.GetTeam(), character.GetTeam())).ToList();
    }

    // Retourne les personnages du camp opposé à celui du personnage donné
    public List<ICharacter> GetOpponents(ICharacter character)
    {
        return characters.Where(c => !AreAllies(c.GetTeam(), character.GetTeam())).ToList();
    }

    // Lance le combat : récupère les personnages et initialise leurs stats de combat
    [ContextMenu("Start Fight")]
    public void StartFight()
    {
        CollectCharacters();
        characters.ForEach(c => c.InitFightStats());
        Debug.Log("Fight started with " + characters.Count + " characters");
    }
}