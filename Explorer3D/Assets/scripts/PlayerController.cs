using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public UnityEvent onJump;
    public UnityEvent onShoot;



    [SerializeField]
    LayerMask groundLayer;
    bool isGrounded;

    Vector2 input = new Vector2();
     public Vector3 moveDir;

    
    

    void Start()
    {
        onJump.AddListener(() => Debug.Log("Player Jumped"));
        onShoot.AddListener(() => Debug.Log("Player Shooted"));
    }



    void Update()
    {
        GroundChecker();
        Vector2 raw = Keyboard.current != null
            ? new Vector2(
                (Keyboard.current.wKey.isPressed ? 1 : 0) +
                (Keyboard.current.sKey.isPressed ? -1 : 0),

                (Keyboard.current.dKey.isPressed ? 1 : 0) +
                (Keyboard.current.aKey.isPressed ? -1 : 0)
            )
            : Vector2.zero;

        input = raw.normalized;
        
        moveDir = new Vector3(input.x, 0, input.y);

        if (isGrounded)
        {
            Debug.Log("Player is on the ground");
        }

        if (Input.GetMouseButtonDown(0))
        {
            onShoot.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onJump.Invoke();
        }
    }


    void GroundChecker()
    {
        Vector3 origin = new Vector3(
            transform.position.x,
            (transform.position.y - transform.localScale.y / 2f) - 0.1f,
            transform.position.z
        );

        isGrounded = Physics.Raycast(origin, Vector3.down, 0.3f, groundLayer);
    }

}
