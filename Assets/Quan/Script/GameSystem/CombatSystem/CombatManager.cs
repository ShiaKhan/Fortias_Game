using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager 
{
    private static CombatManager _instance;
    public PlayerView player;

    private void Awake()
    {
        if (_instance != null)
        {
            return;
        }else _instance = this;
    }

    public void GetPlayerTeam()
    {
        player = GameObject.FindObjectOfType<PlayerView>();
        if (player != null)
        {
            player.getTeam();
        }
        else
        {
            Debug.LogError("PlayerView not found in the scene.");
        }        
    }

    public void GetEnemies()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
