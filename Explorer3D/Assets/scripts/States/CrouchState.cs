using UnityEngine;

public class CrouchState : MovementBaseState
{
 
    public override void EnterState(MovementSateManager movement)
    {
        movement.anim.SetBool("crouch", true);
    }

   public override void UpdateState(MovementSateManager movement)
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)) ExitState(movement, movement.runState);

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(movement.dir.magnitude > 0.1) ExitState(movement, movement.walkState);
            else ExitState(movement, movement.idleState);
        }

        if (movement.returnVerticalInput() < 0) movement.AlterMovementSpeed("cb");
        else movement.AlterMovementSpeed("c");

    }
    void ExitState(MovementSateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("crouch", false);
        movement.SwitchState(state);
    }
}
