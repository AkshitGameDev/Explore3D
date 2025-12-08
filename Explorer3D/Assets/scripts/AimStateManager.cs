using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class AimStateManager : MonoBehaviour
{
    public InputAxis xAxis;
    public InputAxis yAxis;

    [SerializeField] Transform camFollowPos;
    [SerializeField] float sensitivity = 1f;

    void Update()
    {
        if (Mouse.current == null) return;

        // mouse delta
        Vector2 delta = Mouse.current.delta.ReadValue() * 10.0f * sensitivity * Time.deltaTime;

        // yaw (left/right)
        xAxis.Value = xAxis.ClampValue(xAxis.Value + delta.x);

        // pitch (up/down) - usually inverted
        yAxis.Value = yAxis.ClampValue(yAxis.Value - delta.y);
    }

    void LateUpdate()
    {
        // tilt camera pitch around X
        camFollowPos.localEulerAngles = new Vector3(
            yAxis.Value,
            camFollowPos.localEulerAngles.y,
            camFollowPos.localEulerAngles.z
        );

        // rotate player yaw around Y
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            xAxis.Value,
            transform.eulerAngles.z
        );
    }
}
