using System.Collections.Generic;
using Cinemachine;
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
    public CinemachineVirtualCamera virtualCamera;
    private void Start()
    {
        _controller = new CharacterController();
        _controller.playerMovement = this;
        _player = GetComponent<Character>();
        isKing();
        kingCharacter = GetComponentInParent<PlayerView>().getTeam()[0];
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera not found in the scene.");
        }
        
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

        if (_player.Targets == null)
        {
            Debug.Log("No targets found on screen.");
            return;
        }
        else
        {
            if (_player._ownerView.isAutoPlay) _player.autoPlay(_player.Targets);
        }
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
        virtualCamera.Follow = this.transform;
        virtualCamera.LookAt = this.transform;
        
        
    }

    /*private void HandleGravity()
    {
        _isGrounded = _controller.isGrounded;
        
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }*/

    private void UpdateAnimations()
    {
        float speed = new Vector2(_joystick.Direction.x, _joystick.Direction.y).magnitude;
        _animator.SetFloat("MoveSpeed", speed);
    }

    void isKing()
    {
        if (this.transform.GetSiblingIndex() == 0)
        {
            _player._barCharacter.kingView.SetActive(true);
            Camera.main.GetComponent<FollowCamera>().SetTarget(this.transform);
            _joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<DynamicJoystick>();
            this.gameObject.tag = "King";
            _isKing = true;
        }
    }

    public List<Character> GetCharactersOnScreen(Camera cam)
    {
        List<Character> visibleCharacters = new List<Character>();
        Character[] allCharacters = FindObjectsOfType<Character>();
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


