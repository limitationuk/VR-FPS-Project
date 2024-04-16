using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomGun : MonoBehaviour
{
    [SerializeField] Transform muzzlePoint;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;
    [SerializeField] bool isShootable = true;
    [SerializeField] float shootCoolTime;
    [SerializeField] PooledObject hitEffectPrefab;

    public void Fire()
    {
        if (isShootable)
        {
            bool isRay = Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance);

            //hitInfo.collider.gameObject.name  // 어떤 물체에 부딪혔는지
            //hitInfo.point  // 레이가 닿은 위치

            if (isRay)
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.red, 0.3f);  // Ray 그려주기

                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();  // 적 컴포넌트 가져와서

                enemy?.TakeDamage(damage);  // 있으면 데미지 주고 없으면 안주기

                PooledObject hitEffect = Manager.Pool.GetPool(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffect.transform.parent = hitInfo.transform;

                /*Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForceAtPosition(-hitInfo.normal * 1f, hitInfo.point, ForceMode.Impulse);  // 넉백
                }*/
            }
            else
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);
            }

            StartCoroutine(ShootCoolTimeRoutine());
        }
    }

    // 사격 쿨타임 코루틴
    IEnumerator ShootCoolTimeRoutine()
    {
        isShootable = false;
        yield return new WaitForSeconds(shootCoolTime);
        isShootable = true;
    }
}
