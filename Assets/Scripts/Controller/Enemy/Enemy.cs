using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;

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

    // Start is called before the first frame update
    void Start()
    {
        originPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targets = DetectObjectsAround2D(detectionRange, detectionMask);
        target = isDetect ? targets[UnityEngine.Random.Range(0, targets.Count)].transform.position : originPosition;
        MoveToPosition(target);
    }
    
    public void MoveToPosition(Vector3 target)
    {
        Vector3 direction = (target - originPosition).normalized;
        float step = moveSpeed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, target - new Vector3((float)0.3,(float)0.3,0), step);
        
    }

    public List<Character> DetectObjectsAround2D(float radius, LayerMask layerMask)
    {
        List<Character> detectedObjects = new List<Character>();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        if (hitColliders.Length == 0)
        {
            isDetect = false;
        }
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != this.gameObject)
            {
                var character = hitCollider.GetComponent<Character>();
                if (character != null)
                {
                    detectedObjects.Add(character);
                    isDetect = true;
                }
            }
        }
        return detectedObjects;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, detectionRange);
    }
    
}
