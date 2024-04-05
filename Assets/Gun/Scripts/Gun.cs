using System.Collections;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    Rifle,
}

public class Gun : MonoBehaviour
{
    // 탄창 수, 재장전 시간, 연사 속도, 데미지 커스텀

    [Header("총 속성")]

    [Header("총 유형")]
    public WeaponType weaponType;

    [Header("총 최대 사거리")]
    [SerializeField] float maxDistance;

    [Header("총 데미지")]
    [SerializeField] int damage;

    [Header("사격) 사격가능 여부 / 쿨타임")]
    [SerializeField] bool isShootable;
    [SerializeField] float shootCoolTime;

    [Header("연사) 연사가능 여부 / 속도")]
    public bool isContinuousFireable;
    public float fireRate;

    [Header("탄창/탄약-----")]
    [SerializeField] GameObject magazine;
    [SerializeField] int ammoCount;

    [Space(30)]
    [Header("총 이펙트")]

    [Header("총구 섬광")]
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("총알 스파이크 - 물체")]

    [Header("총알 스파이크 - 사람")]

    [Header("탄흔")]
    [SerializeField] PooledObject hitEffectPrefab;

    // private
    [SerializeField] Transform muzzlePoint;
    //[SerializeField] ParticleSystem hitEffect;

    // 사격
    public void Fire()
    {
        if (isShootable)
        {
            muzzleFlash.Play();  // 총구 파티클
            bool isRay = Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance);

            //hitInfo.distance  // 맞았을 때 Ray 쏜 거리
            //hitInfo.collider.gameObject.name  // 어떤 물체에 부딪혔는지
            //hitInfo.point  // 레이가 닿은 위치
            //enemy?.TakeDamage(damage);  // 있으면 데미지 주고 없으면 안주기

            if (isRay)
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.red, 0.3f);  // Ray 그려주기

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

                //ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));  // 총알자국 파티클 (투사체 사용할 때)
                PooledObject hitEffect = Manager.Pool.GetPool(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffect.transform.parent = hitInfo.transform;

                // 넉백
                Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForceAtPosition(-hitInfo.normal * 0.1f, hitInfo.point, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);
                Debug.Log("안 맞음");
            }

            StartCoroutine(ShootCoolTimeRoutine());
        }
    }

    // 사격쿨타임 코루틴
    IEnumerator ShootCoolTimeRoutine()
    {
        isShootable = false;
        yield return new WaitForSeconds(shootCoolTime);
        isShootable = true;
    }

    // 탄창, 탄약 계산
}
