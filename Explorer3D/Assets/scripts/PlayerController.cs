using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public UnityEvent onJump;
    public UnityEvent onShoot;

    [SerializeField] Animator animator;

    [SerializeField] LayerMask groundLayer;
    bool isGrounded;

    public float minSpeed = 0.5f;
    public float maxSpeed = 5f;
    public float accelerationTime = 0.5f;

    float currentSpeed = 0f;
    float speedVelocity = 0f;   // for SmoothDamp

    Vector2 input;
    Vector3 moveDir;

    void Start()
    {
        onJump.AddListener(() => Debug.Log("Player Jumped"));
        onShoot.AddListener(() => Debug.Log("Player Shooted"));
    }

    void Update()
    {
        GroundChecker();
        ReadMovementInput();
        UpdateWalkingState();
        HandleShooting();
        HandleJump();
    }

    void FixedUpdate()
    {
        Vector3 move = moveDir * currentSpeed * Time.fixedDeltaTime;
        transform.Translate(move, Space.World);
    }

    // -------------------------------
    //  INPUT SYSTEM
    // -------------------------------
    void ReadMovementInput()
    {
        if (Keyboard.current == null)
        {
            input = Vector2.zero;
            return;
        }

        float forward = 0f;
        float right = 0f;

        if (Keyboard.current.wKey.isPressed) forward += 1;
        if (Keyboard.current.sKey.isPressed) forward -= 1;

        if (Keyboard.current.dKey.isPressed) right += 1;
        if (Keyboard.current.aKey.isPressed) right -= 1;

        input = new Vector2(forward, right).normalized;
        moveDir = new Vector3(right, 0, forward);   // correct XZ movement
    }

    // -------------------------------
    //  WALKING LOGIC + ANIMATOR
    // -------------------------------
    void UpdateWalkingState()
    {
        bool isMoving = input.magnitude > 0.1f;

        animator.SetBool("walk", isMoving);

        float targetSpeed = isMoving ? maxSpeed : 0f;

        // Accelerate/decelerate smoothly in 0.5 seconds
        currentSpeed = Mathf.SmoothDamp(
            currentSpeed,
            targetSpeed,
            ref speedVelocity,
            accelerationTime
        );

        // Prevent tiny speed drops from causing jitter
        if (!isMoving && currentSpeed < 0.1f)
            currentSpeed = 0f;

        // Clamp between minSpeed and maxSpeed while moving
        if (isMoving)
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
    }

    void HandleShooting()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            onShoot.Invoke();
    }

    void HandleJump()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            onJump.Invoke();
    }

    void GroundChecker()
    {
        Vector3 origin = new Vector3(
            transform.position.x,
            transform.position.y - (transform.localScale.y / 2f) - 0.1f,
            transform.position.z
        );

        isGrounded = Physics.Raycast(origin, Vector3.down, 0.3f, groundLayer);
    }
}
