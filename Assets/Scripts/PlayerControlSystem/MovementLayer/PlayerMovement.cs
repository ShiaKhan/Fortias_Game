using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float _moveSpeed = 5f;


    [Header("References")]
    [SerializeField] private DynamicJoystick _joystick;
    [SerializeField] private Animator _animator;
    //[SerializeField] private Joystick _joystickMovement;


    public CharacterController _controller;
    public bool _isKing = false;
    private Character _player;
    private Character kingCharacter;
    public Camera cam ;
    [SerializeField]
    private PlayerView _playerView;
    private void Start()
    {
        _controller = new CharacterController();
        _controller.playerMovement = this;
        _player = GetComponent<Character>();
        Debug.Log("Player Movement Initialized for: " + this.transform.GetSiblingIndex());
        _playerView = transform.GetComponentInParent<PlayerView>();
        kingCharacter = _playerView.getKing();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Camera not found in the scene.");
        }
        isKing();
    }

    private void FixedUpdate()
    {

        if (_isKing)
        {
            HandleMovement();
        }
        else
        {
            _controller.followKing(kingCharacter);
        }
        _player.Targets = GetCharactersOnScreen(Camera.main);
        _player.Enemies = GetAlldObjects();
        _player.ObjectOnScreen = GetEnemiesAndObjectsOnScreen(Camera.main);
        _player.autoPlay(_player.Targets, _player.Enemies,_player.ObjectOnScreen);
        if (_player == null)
        {
            Debug.LogError("_player is null!");
            return;
        }
        if (_player.Targets == null)
        {
            Debug.Log("No targets found on screen.");
            return;
        }
        if (_player._ownerView == null)
        {
            Debug.LogError("_player._ownerView is null!");
            return;
        }
        //HandleGravity();
        //UpdateAnimations();
    }

    private void HandleMovement()
    {
        if (_player._ownerView.isAutoPlay)
        {
            _joystick.gameObject.SetActive(false);

        }
        else
        {
            _joystick.gameObject.SetActive(true);
            Vector2 joystickDirection = _joystick.Direction;
            Vector3 moveDirection = new Vector3(joystickDirection.x, joystickDirection.y, 0);

            if (moveDirection != Vector3.zero)
            {
                if (moveDirection.x < 0)
                {
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                // Movement
                _controller.Move(moveDirection * _moveSpeed * Time.deltaTime);
            }
        }
        

    }
    private void UpdateAnimations()
    {
        float speed = new Vector2(_joystick.Direction.x, _joystick.Direction.y).magnitude;
        _animator.SetFloat("MoveSpeed", speed);
    }

    void isKing()
    {
        Debug.Log("Is King: " + _isKing + " for player: " + this.name);
        if (this.transform.GetSiblingIndex() == 0)
        {
            _player._barCharacter.kingView.SetActive(true);
            
            Debug.Log("Player is King: " + this.name);
            Camera.main.GetComponent<FollowCamera>().SetTarget(this.transform);
            _joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<DynamicJoystick>();
            this.gameObject.tag = "King";
            _isKing = true;
        }
        
    }

    public List<Character> GetCharactersOnScreen(Camera cam)
    {
        List<Character> visibleCharacters = new List<Character>();
        Character[] allCharacters = Object.FindObjectsByType<Character>(FindObjectsSortMode.None);
        foreach (Character character in allCharacters)
        {
            Vector3 viewportPos = cam.WorldToViewportPoint(character.transform.position);
            if (viewportPos.z > 0 && viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                if (character._ownerView != _player._ownerView && character != null)
                {
                    visibleCharacters.Add(character);
                }
            }
        }
        return visibleCharacters;
    }
    public List<GameObject> GetEnemiesAndObjectsOnScreen(Camera cam)
    {
        List<GameObject> visibleEnemiesAndObjects = new List<GameObject>();
        GameObject[] allCharacters = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject character in allCharacters)
        {
            Vector3 viewportPos = cam.WorldToViewportPoint(character.transform.position);
            if (viewportPos.z > 0 && viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                if(character.CompareTag("Enemy") || character.CompareTag("Tree"))
                {
                    visibleEnemiesAndObjects.Add(character);
                }
            }
        }
        visibleEnemiesAndObjects.Sort((a, b) =>
        Vector3.Distance(this.transform.position, a.transform.position)
        .CompareTo(Vector3.Distance(this.transform.position, b.transform.position))
        );
        return visibleEnemiesAndObjects;
    }
    public List<GameObject> GetAlldObjects()
    {
        List<GameObject> visiblObjects = new List<GameObject>();
        GameObject[] allCharacters = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject character in allCharacters)
        {
            if(character.CompareTag("Enemy") || character.CompareTag("Tree"))
            {
                visiblObjects.Add(character);
            }
        }
        visiblObjects.Sort((a, b) =>
        Vector3.Distance(this.transform.position, a.transform.position)
        .CompareTo(Vector3.Distance(this.transform.position, b.transform.position))
        );
        return visiblObjects;
    }
    public void cleanTargets()
    {
        foreach (var target in _player.Targets)
        {
            foreach (var item in _player.Team)
            {
                if (target == item.gameObject)
                {
                    _player.Targets.Remove(target);
                }
            }
        }
    }
    public void clearTargets()
    {
        _player.Targets.Clear();
    }

    
}


