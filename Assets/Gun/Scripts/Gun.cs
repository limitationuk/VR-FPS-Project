using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // 탄창 수, 재장전 시간, 연사 속도, 데미지 커스텀

    [SerializeField] Transform shootPoint;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;
    [SerializeField] ParticleSystem shootFlash;
    //[SerializeField] ParticleSystem hitEffect;

    [SerializeField] PooledObject hitEffectPrefab;

    [SerializeField] float shootCoolTime;
    [SerializeField] bool isShootable;

    private void Start()
    {
        //Manager.Pool.CreatePool(hitEffectPrefab, 5, 5);
    }

    public void Fire()
    {
        if (isShootable)
        {
            shootFlash.Play();  // 총구 파티클
            bool isRay = Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance);

            //hitInfo.distance  // 맞았을 때 Ray 쏜 거리
            //hitInfo.collider.gameObject.name  // 어떤 물체에 부딪혔는지
            //hitInfo.point  // 레이가 닿은 위치
            //enemy?.TakeDamage(damage);  // 있으면 데미지 주고 없으면 안주기

            if (isRay)
            {
                Debug.DrawRay(shootPoint.position, shootPoint.forward * hitInfo.distance, Color.red, 0.3f);  // Ray 그려주기

                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();  // 적 컴포넌트 가져와서

                // 머리 맞은 경우
                if ((1 << hitInfo.collider.gameObject.layer) == LayerMask.GetMask("Head"))  // Layer6: Head (64 == 64)
                {
                    Debug.Log("머리 맞음");
                    enemy = hitInfo.collider.GetComponentInParent<Enemy>();
                    enemy.TakeDamage(enemy.Hp);  // damage를 적 HP로 설정해서 한번에 죽게
                }
                else if ((1 << hitInfo.collider.gameObject.layer) == LayerMask.GetMask("Body"))  // Layer7: Head (128 == 128)
                {
                    Debug.Log("몸 맞음");
                    enemy = hitInfo.collider.GetComponentInParent<Enemy>();
                    enemy.TakeDamage(damage);  // 총의 기본 damage 적용
                }

                //ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));  // 총알자국 파티클
                PooledObject hitEffect = Manager.Pool.GetPool(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffect.transform.parent = hitInfo.transform;

                Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForceAtPosition(-hitInfo.normal * 1f, hitInfo.point, ForceMode.Impulse);  // 넉백
                }
            }
            else
            {
                Debug.DrawRay(shootPoint.position, shootPoint.forward * maxDistance, Color.red, 0.3f);
                Debug.Log("안 맞음");
            }

            StartCoroutine(ShootCoolTimeRoutine());
        }
    }

    IEnumerator ShootCoolTimeRoutine()
    {
        isShootable = false;
        yield return new WaitForSeconds(shootCoolTime);
        isShootable = true;
    }
}
