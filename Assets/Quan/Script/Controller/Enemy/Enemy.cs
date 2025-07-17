using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;

    public float range;
    public Vector3 originPosition;
    public bool isDetect = false;

    public Vector3 target;
    public Vector3 playerPosition;
    private IObjectPool<Enemy> pool;
    [SerializeField] private float detectionRange = 10;
    [SerializeField] private LayerMask detectionMask;
    public float damage;
    public Animator enemy;
    public List<Character> targets;
    private PlayerView playerView;
    public void setPool(IObjectPool<Enemy> pool)
    {
        this.pool = pool;
    }
    // Start is called before the first frame update
    void Start()
    {
        originPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        target = isDetect? playerPosition : originPosition;
        MoveToPosition(target);
        DetectPlayer();
    }
    
    public void MoveToPosition(Vector3 target)
    {
        Vector3 direction = (target - originPosition).normalized;
        float step = moveSpeed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, target - new Vector3((float)0.3,(float)0.3,0), step);
        
    }

    public void DetectPlayer()
    {
        playerView = GameObject.FindObjectOfType<PlayerView>();
        if (playerView == null)
        {
            Debug.LogError("PlayerView not found in the scene.");
            return;
        }else
        {
            targets = playerView.getTeam();
        }
    }
    //Draw a circle around this object  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isDetect = true;
            playerPosition = other.transform.position;
            //Debug.Log("in");
            //pool.Release(this);
            enemy.SetTrigger("Atk");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isDetect = false;
            Debug.Log("out");
        }
    }
    


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, detectionRange);
    }
    
}
