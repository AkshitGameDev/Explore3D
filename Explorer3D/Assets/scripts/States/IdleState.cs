using Unity.VisualScripting;
using UnityEngine;

public class IdleState : MovementBaseState
{

    public override void EnterState(MovementSateManager movement)
    {

    }

    public override void UpdateState(MovementSateManager movement)
    {
        if (movement.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) movement.SwitchState(movement.runState);
            else movement.SwitchState(movement.walkState);
        }
        if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftControl)) movement.SwitchState(movement.crouchState);
        
    }
}
