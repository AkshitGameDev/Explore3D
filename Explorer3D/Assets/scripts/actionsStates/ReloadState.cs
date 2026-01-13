using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.rhandAim.weight = 0;
        actions.LhandIk.weight = 0;
        actions.anim.SetTrigger("reload");
        Debug.Log("Reloading...");


    }
    public override void UpdateState(ActionStateManager actions)
    {

    }
    



}
