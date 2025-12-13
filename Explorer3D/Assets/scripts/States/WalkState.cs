using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementSateManager movement)
    {
        movement.anim.SetBool("walk", true);

    }

    public override void UpdateState(MovementSateManager movement)
    {
        if(Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.runState);
        else if (Input.GetKey(KeyCode.LeftControl)) ExitState(movement, movement.crouchState);
        else if (movement.dir.magnitude < 0.1f) ExitState(movement, movement.idleState);

        if(movement.returnVerticalInput() < 0) movement.AlterMovementSpeed("wb");
        else movement.AlterMovementSpeed("w"); 


    }

    void ExitState(MovementSateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("walk", false);
        movement.SwitchState(state);
    }
}
