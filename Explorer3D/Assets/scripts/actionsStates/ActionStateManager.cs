using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    ActionBaseState currentState;
    public ReloadState reloadState = new ReloadState();
    public DefaltState defaltState = new DefaltState();

    public GameObject currentWepon;
    [HideInInspector] public WeponAmmo ammo;

    [HideInInspector] public Animator anim;

    public MultiAimConstraint rhandAim;

    public TwoBoneIKConstraint LhandIk;

    private void Start()
    {
        SwitchState(defaltState);
        ammo = currentWepon.GetComponent<WeponAmmo>();
        anim = GetComponent<Animator>();
        //Debug.Log("ammo in action manager: " + ammo.currentAmmo);
    }

    public void SwitchState(ActionBaseState state)
    { 
        currentState = state;
        state.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void WeponReloaded()
    {
        ammo.Reload();
        SwitchState(defaltState);
        //Debug.Log("Reload compleate...");
    }

    public void PlayReloadingSound()
    {
        AudioManager.instance.PlayOneShot(AudioManager.instance.reloadClip);
    }
   
}
