using UnityEngine;

public class RunState : MovementBaseState
{

    public override void EnterState(MovementSateManager movement)
    {
        movement.anim.SetBool("run", true);
    }

    public override void UpdateState(MovementSateManager movement)
    {
        if(Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.walkState);
        else if(movement.dir.magnitude == 0) ExitState(movement, movement.idleState);

        if (movement.returnVerticalInput() < 0) movement.AlterMovementSpeed("rb");
        else movement.AlterMovementSpeed("r");

    }
    void ExitState(MovementSateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("run", false);
        movement.SwitchState(state);
    }
}
