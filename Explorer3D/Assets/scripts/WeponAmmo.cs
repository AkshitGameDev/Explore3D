using UnityEngine;

public class WeponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public int currentAmmo;

    void Start()
    {
        currentAmmo = clipSize;
    }

    

    public void Reload()
    {
        if( extraAmmo >= clipSize)
        {
            int ammoNeeded = clipSize - currentAmmo;
            extraAmmo -= ammoNeeded;
            currentAmmo += ammoNeeded;
        }
        else if(extraAmmo > 0)
        {
            if(currentAmmo + extraAmmo > clipSize)
            {
                int leftoverAmmo = currentAmmo + extraAmmo - clipSize;
                extraAmmo = leftoverAmmo;
                currentAmmo = clipSize;

            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }


}
  