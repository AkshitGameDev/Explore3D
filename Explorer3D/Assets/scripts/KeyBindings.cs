using UnityEngine;

public class KeyBindings : MonoBehaviour
{

    KeyCode Forward = KeyCode.W;
    KeyCode Backwards = KeyCode.S;
    KeyCode Left = KeyCode.A;
    KeyCode Right = KeyCode.D;
    KeyCode Sprint = KeyCode.LeftShift;
    KeyCode crouch = KeyCode.LeftControl;
    KeyCode Shoot = KeyCode.Mouse0;

    public static KeyBindings instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public KeyCode GetForwardKey() { return Forward; }
    public KeyCode GetBackwardsKey() { return Backwards; }
    public KeyCode GetLeftKey() { return Left; }

    public KeyCode GetRightKey() { return Right; }
    public KeyCode GetSprintKey() { return Sprint; }
    public KeyCode GetCrouchKey() { return crouch; }
    public KeyCode GetShootKey() { return Shoot; }





}
