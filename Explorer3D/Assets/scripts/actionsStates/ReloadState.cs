using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.anim.SetTrigger("reload");
    }
    public override void UpdateState(ActionStateManager actions)
    {

    }

}
