using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform targetToPlayer;

    public Tilemap tileMap;

    public Vector3 offset;
    private Vector3 topRightLimit;
    private Vector3 bottomLeftLimit;

    private float haftHeight;
    private float haftWidth;
    void Start()
    {
        targetToPlayer = PlayerController.instance.transform;

        Camera cam = Camera.main;
        haftHeight = cam.orthographicSize;
        haftWidth = haftHeight * cam.aspect;

        offset = Camera.main.transform.position;

        Bounds bounds = tileMap.localBounds;
        topRightLimit = bounds.max;
        bottomLeftLimit = bounds.min;


    }
    

    private void LateUpdate()
    {
        transform.position = new Vector3(targetToPlayer.position.x, targetToPlayer.position.y, transform.position.z);

        Vector3 targetPot = targetToPlayer.position + offset;
        float clampX = Mathf.Clamp(targetPot.x, bottomLeftLimit.x + haftWidth, topRightLimit.x - haftWidth);
        float clampY = Mathf.Clamp(targetPot.y, bottomLeftLimit.y + haftHeight, topRightLimit.y - haftHeight);

        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }
    
   

    
}
