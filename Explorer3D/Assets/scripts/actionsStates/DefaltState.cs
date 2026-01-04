using UnityEngine;

public  class DefaltState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {

    }

    public override void UpdateState(ActionStateManager actions)
    {
        if (CanReoad(actions) && Input.GetKeyDown(KeyCode.R))
        {
            actions.SwitchState(actions.reloadState);
        }
    }

    bool CanReoad(ActionStateManager actions)
    {
        if (actions.ammo.currentAmmo == actions.ammo.clipSize) return false;
        if (actions.ammo.extraAmmo == 0) return false;
        else return true;

    }
}
