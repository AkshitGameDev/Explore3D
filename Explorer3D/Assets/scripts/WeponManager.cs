using UnityEngine;

public class WeponManager : MonoBehaviour
{
    [Header("FireRate")]
    [SerializeField]float fireRate;
    float fireRateTimer;
    [SerializeField]bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform barrelPosition;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    AimStateManager aim;

    KeyCode shoot = KeyBindings.instance.GetShootKey();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aim = GetComponentInParent<AimStateManager>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldFire())
        {
            Fire();
        }
    }

    bool ShouldFire()
    {

        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate)
        {
            return false;
        }
        if(semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if(!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;

        return false;
    }

    void Fire()
    {
        fireRateTimer = 0f;
        barrelPosition.LookAt(aim.aimPos);
        for(int i = 0; i < bulletPerShot; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, barrelPosition.position, barrelPosition.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
