using UnityEngine;

public class WeponManager : MonoBehaviour
{
    [SerializeField]
    float fireRate;

    float fireRateTimer;

    [SerializeField]
    bool semiAuto;

    KeyCode shoot = KeyBindings.instance.GetShootKey();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool ShouldFire()
    {
        if(fireRateTimer < fireRate)
        {
            fireRateTimer += Time.deltaTime;
            return false;
        }

        if(semiAuto && Input.GetKeyDown(shoot)) return true;
        if(!semiAuto && Input.GetKeyDown(shoot)) return true;

        return false;
    }

    void Fire()
    {

    }
}
