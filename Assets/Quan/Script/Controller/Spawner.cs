using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float timespawn;

    private float timeLastSpawn;
	
    [SerializeField] private Enemy enemyPrefab;
	[SerializeField] private int countEnemy;
    [SerializeField] private int maxEnemy;
    private IObjectPool<Enemy> enemyPool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        enemyPool = new ObjectPool<Enemy>(CreateEnemy);
    }

    public Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab,spawnPoints[0].position,Quaternion.identity,this.gameObject.transform);
        return enemy;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeLastSpawn && countEnemy < maxEnemy)
        {
            enemyPool.Get();
            timeLastSpawn = Time.time + timespawn;
            countEnemy++;
        }
    }
}
