using UnityEngine;

public class WeponAmmo : MonoBehaviour
{
    public float clipSize;
    public float extraAmmo;
    [HideInInspector] public float currentAmmo;

    void Start()
    {
        currentAmmo = clipSize;
    }

    
}
