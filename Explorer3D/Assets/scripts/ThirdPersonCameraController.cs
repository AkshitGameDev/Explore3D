using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;          // player root
    public Transform camPlaceholder;  // your "camplaceholder" under player

    [Header("Camera Offset (local to placeholder)")]
    public Vector3 localOffset = new Vector3(0f, 0.7f, -3.2f);

    [Header("Rotation")]
    public float mouseSensitivity = 1.5f;
    public float maxPitch = 60f;
    public float minPitch = -40f;
    public float rotationSpeedDegPerSec = 150f; // 15 deg per 0.1s

    float targetYaw;
    float targetPitch;
    float currentYaw;
    float currentPitch;

    void Start()
    {
        if (camPlaceholder == null && player != null)
            camPlaceholder = player;

        // start facing player forward
        currentYaw = targetYaw = player != null ? player.eulerAngles.y : 0f;
        currentPitch = targetPitch = 0f;

        ForceCameraToPlaceholder();
    }

    void LateUpdate()
    {
        if (player == null || camPlaceholder == null) return;

        HandleInput();
        SmoothAngles();
        UpdateCamera();
        RotatePlayer();
    }

    void HandleInput()
    {
        if (Mouse.current == null) return;

        Vector2 delta = Mouse.current.delta.ReadValue();
        delta *= mouseSensitivity * Time.deltaTime;

        targetYaw += delta.x;
        targetPitch -= delta.y;
        targetPitch = Mathf.Clamp(targetPitch, minPitch, maxPitch);
    }

    void SmoothAngles()
    {
        currentYaw = Mathf.MoveTowardsAngle(
            currentYaw, targetYaw,
            rotationSpeedDegPerSec * Time.deltaTime
        );

        currentPitch = Mathf.MoveTowards(
            currentPitch, targetPitch,
            rotationSpeedDegPerSec * Time.deltaTime
        );
    }

    void UpdateCamera()
    {
        Quaternion camRot = Quaternion.Euler(currentPitch, currentYaw, 0f);

        // position = placeholder position + localOffset in its local space
        Vector3 targetPos = camPlaceholder.TransformPoint(localOffset);

        transform.position = targetPos;
        transform.rotation = camRot;
    }

    void RotatePlayer()
    {
        Quaternion targetPlayerRot = Quaternion.Euler(0f, currentYaw, 0f);
        player.rotation = Quaternion.RotateTowards(
            player.rotation,
            targetPlayerRot,
            rotationSpeedDegPerSec * Time.deltaTime
        );
    }

    void ForceCameraToPlaceholder()
    {
        if (camPlaceholder == null) return;
        transform.position = camPlaceholder.TransformPoint(localOffset);
        transform.rotation = Quaternion.LookRotation(
            player.position - transform.position, Vector3.up
        );
    }
}
