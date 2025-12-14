using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class AimStateManager : MonoBehaviour
{

    AimBaseState currentState;
    public HipFireState hip = new HipFireState();
    public AimState aim = new AimState();
    public InputAxis xAxis;
    public InputAxis yAxis;

    [SerializeField] Transform camFollowPos;
    [SerializeField] float sensitivity = 1f;

    [HideInInspector] public Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        Debug.Log("AimStateManager Awake: ", anim);
    }

    void Start()
    {
        SwitchState(hip);
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // mouse delta
        Vector2 delta = Mouse.current.delta.ReadValue() * 10.0f * sensitivity * Time.deltaTime;

        // yaw (left/right)
        xAxis.Value = xAxis.ClampValue(xAxis.Value + delta.x);

        // pitch (up/down) - usually inverted
        yAxis.Value = yAxis.ClampValue(yAxis.Value - delta.y);

        currentState.UpdateState(this);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }


    void LateUpdate()
    {
        // Clamp pitch (camera up/down)
        float pitch = yAxis.Value;
        pitch = Mathf.Clamp(pitch, -89.9f, 89.9f);

        camFollowPos.localRotation = Quaternion.Euler(
            pitch,
            0f,
            0f
        );

        // Wrap yaw (player left/right) — infinite rotation
        float yaw = xAxis.Value;

        if (yaw > 180f) yaw -= 360f;
        if (yaw < -180f) yaw += 360f;

        transform.rotation = Quaternion.Euler(
            0f,
            yaw,
            0f
        );
    }

}
