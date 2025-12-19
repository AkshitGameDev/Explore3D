using UnityEngine;
using System;
public class MovementSateManager : MonoBehaviour
{
    
    public float currentMoveSpeed;
    public float walkSpeed = 3f, walkBackSpeed = 1.8f;
    public float runSpeed = 6.2f, runBackSpeed = 4.8f;
    public float crouchSpeed = 1.8f, crouchBackSpeed = 1;
    float hzInput, vInput;
    [HideInInspector] public Vector3 dir;
    CharacterController controller;
    [SerializeField]
    float groundYoffset;
    [SerializeField]
    LayerMask groundMask;
    Vector3 spearPos;

    [SerializeField]
    float gravity = -9.81f;
    Vector3 velocity;

    MovementBaseState currentState;
    public IdleState idleState = new IdleState();
    public WalkState walkState = new WalkState();
    public RunState runState = new RunState();
    public CrouchState crouchState = new CrouchState();

    [HideInInspector] public Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(idleState);
        currentMoveSpeed = walkSpeed;
    }

    public float returnVerticalInput()
    {
        return vInput;
    }

    public void SwitchState(MovementBaseState state)
    {
        Debug.Log("Switched to " + state.GetType().Name);

        currentState = state;
        currentState.EnterState(this);
    }

    public void AlterMovementSpeed(String speedType)
    {
        // W - walk,WB - walk back, R - run, RB - run back, C - crouch, CB - crouch back
        // if you didnt get why this function exists yout Stupid Noob, this function exist to alter the movement speed based on the speed type provided.

        switch (speedType)
        {
            case "w":
                currentMoveSpeed = walkSpeed;
                break;
            case "wb":
                currentMoveSpeed = walkBackSpeed;
                break;
            case "r":
                currentMoveSpeed = runSpeed;
                break;
            case "rb":
                currentMoveSpeed = runBackSpeed;
                break;
            case "c":
                currentMoveSpeed = crouchSpeed;
                break;
            case "cb":
                currentMoveSpeed = crouchBackSpeed;
                break;
            default:
                Debug.Log("Invalid speed type");
                break;
        }
    }



    void Update()
    {
        GetDirectionsAndMove();
        ApplyGravity();

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    void GetDirectionsAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        dir = transform.forward * vInput + transform.right * hzInput;
        controller.Move(dir * currentMoveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spearPos = new Vector3(transform.position.x, transform.position.y - groundYoffset, transform.position.z);
        if (Physics.CheckSphere(spearPos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void ApplyGravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2f;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spearPos, controller.radius - 0.05f);
    }

}
