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
     public CinemachineCamera vCam;
    public float adsFov = 40f;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float curFov;
    public float shoothSpeed = 10f;

    [SerializeField] Transform aimPos;  
    [SerializeField] float aimSmoothSpeed = 20f;
    [SerializeField] LayerMask aimMask;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Debug.Log("AimStateManager Awake: ", anim);
        vCam = GetComponentInChildren<CinemachineCamera>();

        if (vCam == null)
            Debug.LogError("CinemachineVirtualCamera not found!");
    }

    void Start()
    {
        SwitchState(hip);
        hipFov = vCam.Lens.FieldOfView;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView, curFov, Time.deltaTime * shoothSpeed);

        // mouse delta
        Vector2 delta = Mouse.current.delta.ReadValue() * 10.0f * sensitivity * Time.deltaTime;

        // yaw (left/right)
        xAxis.Value = xAxis.ClampValue(xAxis.Value + delta.x);

        // pitch (up/down) - usually inverted
        yAxis.Value = yAxis.ClampValue(yAxis.Value - delta.y);

        Vector2 screenCenter = new Vector2(Screen.width, Screen.height) / 2f;
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        currentState.UpdateState(this);

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask)) aimPos.position = Vector3.Lerp(aimPos.position, hit.point, Time.deltaTime * aimSmoothSpeed);
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
