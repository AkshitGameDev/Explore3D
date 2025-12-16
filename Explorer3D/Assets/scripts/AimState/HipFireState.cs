using UnityEngine;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("aiming", false);
        aim.curFov = aim.hipFov;

    }
    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("Switching to Aim State");
            aim.SwitchState(aim.aim);
        }

    }


}
