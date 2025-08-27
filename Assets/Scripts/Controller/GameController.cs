using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject player;
    public GameObject playerPrefab;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("GameController Initialized.");
        if (player != null)
        {
            Destroy(player);
        }
        initPlayer(playerPrefab, SceneManager.GetActiveScene().name);

        
        DontDestroyOnLoad(gameObject);

    }
    public void initPlayer(GameObject playerPrefab,string sceneName )
    {
        if (player == null && sceneName == "ThiTran")
        {
            Debug.Log("Player not found, spawning new player.");
            player = Instantiate(playerPrefab, transform.position, quaternion.identity);
            Debug.Log("Player spawned.");
            player.tag = "Player";
            player.AddComponent<PlayerController>();
        }
        if (player == null && sceneName == "KhuVuc")
        {
            Debug.Log("Player not found, spawning new player.");
            player = Instantiate(playerPrefab, transform.position, quaternion.identity);
            Debug.Log("Player spawned.");
            player.tag = "Player";
            player.AddComponent<PlayerView>();
        }
        CameraController.instance.setTarget(player.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeScene(string sceneName)
    {
        if (player != null)
        {
            Destroy(player);
            player = null;
        }
        SceneManager.LoadScene(sceneName);
        initPlayer(playerPrefab, sceneName);
    }
}
