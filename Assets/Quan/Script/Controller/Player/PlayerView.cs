using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private List<HeroesDataSO> heroes_bag;
    [SerializeField] private List<HeroesDataSO> heroes_bag_selected;
    public Transform spawnHeroPoint;
    public GameObject prefabHero;
    [SerializeField] private List<Character> team;
    public bool isAutoPlay = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Init();
        spawnHero(heroes_bag_selected);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Init()
    {
        this.heroes_bag = playerData._heroes;
        if (playerData == null) Resources.Load<PlayerDataSO>("PlayerDataSO");
    }

    public void spawnHero(List<HeroesDataSO> heroes)
    {
        foreach (HeroesDataSO hero in heroes)
        {
            Vector3 spawnPosition = new Vector3(Random.Range((float)(spawnHeroPoint.position.x - 0.5), (float)(spawnHeroPoint.position.x + 0.5)), Random.Range((float)(spawnHeroPoint.position.y - 0.5), (float)(spawnHeroPoint.position.y + 0.5)), spawnHeroPoint.transform.position.z);
            Character heroSpawned = Instantiate(prefabHero, spawnPosition, Quaternion.identity, this.transform).GetComponent<Character>();
            heroSpawned.InitCharacter(hero, this);
            team.Add(heroSpawned);
        }
        refeshTeam();
    }
    public void clickAutoPlay()
    {
        isAutoPlay = !isAutoPlay;
    }
    public void updateTeam(Character character)
    {
        if (team.Contains(character))
        {
            team.Remove(character);
            Debug.Log("Remove character: " + character.name);
            refeshTeam();
        }

    }
    public void refeshTeam()
    {
        foreach (Character child in team)
        {
            if (child != null)
            {
                child.getTeams();
            }
        }
    }
    public List<Character> getTeam()
    {
        return team;
    }   
}
