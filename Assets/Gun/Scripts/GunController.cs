using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("���� ������ ��")]
    [SerializeField] Gun currentGun;

    // private
    [SerializeField] float currentFireRate;

    void Start()
    {
        currentFireRate = 0;
    }

    // �� ���� �ĺ�
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

    // ����ӵ� ���
    private void FireRateCalc()
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    // ����
    private void ContinuousFire()
    {
        if (currentFireRate <= 0)
        {
            currentFireRate = currentGun.fireRate;
            currentGun.Fire();
        }
    }
}
