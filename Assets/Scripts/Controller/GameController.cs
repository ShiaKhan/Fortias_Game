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
    public GameObject heroPrefab;
    public GameObject playerViewPrefab;

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
            Debug.Log("Player not found, spawning new player in scene: " + sceneName);
            player = Instantiate(playerPrefab, transform.position, quaternion.identity);
            Debug.Log("Player spawned.");
            player.tag = "Player";
            player.AddComponent<PlayerController>();
        }
        if (player == null && sceneName == "Mockup")
        {
            Debug.Log("Player not found, spawning new player in scene: " + sceneName);
            player = Instantiate(playerViewPrefab, transform.position, quaternion.identity);
            Debug.Log("Player spawned.");
            player.tag = "Player";
            var playerView = player.AddComponent<PlayerView>();
            playerView.prefabHero = heroPrefab;
            playerView.GetComponent<PlayerView>().enabled = true;
        }
        FollowCamera.instance.SetTarget(player.transform);
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
        // Đăng ký callback khi scene load xong
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Chỉ khởi tạo player khi scene đã load xong
        initPlayer(playerPrefab, scene.name);
        // Hủy đăng ký callback để tránh gọi nhiều lần
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
