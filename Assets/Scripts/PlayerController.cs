using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float moveX;
    private float moveY;

    private Vector3 minLimit;
    private Vector3 maxLimit;

    public Tilemap map;

    public float moveSpeed = 1f;

    public Animator anim;

    public static PlayerController instance;
    public string areaEntrance;


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        BoundsInt bounds = map.cellBounds;
        minLimit = map.CellToWorld(bounds.min) + new Vector3(0.5f, 1f, 0f);
        maxLimit = map.CellToWorld(bounds.max) + new Vector3(-0.5f, -1f, 0f);

    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        rigidbody2d.linearVelocity = new Vector2(moveX, moveY) * moveSpeed;


        //move UP_DOWN_LEFT_RIGHT
        anim.SetFloat("moveX", rigidbody2d.linearVelocity.x);
        anim.SetFloat("moveY", rigidbody2d.linearVelocity.y);

        //Facing UP_DOWN_LEFT_RIGHT
        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            anim.SetFloat("turnX", moveX);
            anim.SetFloat("turnY", moveY);
        }

        Vector3 tagetPos = transform.position;

        float clampX = Mathf.Clamp(tagetPos.x, minLimit.x, maxLimit.x);
        float clampY = Mathf.Clamp(tagetPos.y, minLimit.y, maxLimit.y);

        transform.position = new Vector3(clampX, clampY, transform.position.z);


    }
    


    
}
