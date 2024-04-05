using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("현재 장착된 총")]
    [SerializeField] Gun currentGun;

    // private
    [SerializeField] float currentFireRate;

    void Start()
    {
        currentFireRate = 0;
    }

    // 총 유형 식별
    public void WeaponFire()
    {
        switch (currentGun.weaponType)
        {
            case WeaponType.Pistol:
                PistolFire();
                break;
            case WeaponType.Rifle:
                RifleFire();
                break;
            default:
                break;
        }
    }

    public void PistolFire()
    {
        currentGun.Fire();
    }

    public void RifleFire()
    {
        FireRateCalc();
        ContinuousFire();
    }

    // 연사속도 계산
    private void FireRateCalc()
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    // 연사
    private void ContinuousFire()
    {
        if (currentFireRate <= 0)
        {
            currentFireRate = currentGun.fireRate;
            currentGun.Fire();
        }
    }
}
