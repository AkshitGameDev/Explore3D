using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("aiming", true);

    }
    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Debug.Log("Switching to Hip Fire");
            aim.SwitchState(aim.hip);
        }
    }


}
