using System.Collections;
using System.Collections.Generic;
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

    [Tooltip("총 유형")]
    [SerializeField] WeaponType weaponType;

    [Tooltip("총구 위치")]
    [SerializeField] Transform muzzlePoint;
    [Tooltip("최대 사거리")]
    [SerializeField] float maxDistance;
    [Tooltip("공격력")]
    [SerializeField] int damage;

    [Tooltip("사격 가능 여부")]
    [SerializeField] bool isShootable;
    [Tooltip("사격 쿨타임")]
    [SerializeField] float shootCoolTime;
    [Tooltip("연사 속도")]
    [SerializeField] float fireRate;

    [Space(20)]    
    [Header("이펙트 / 오디오")]

    [Tooltip("총구 섬광")]
    [SerializeField] ParticleSystem muzzleFlash;
    [Tooltip("피격 위치 이펙트")]
    [SerializeField] PooledObject hitEffectPrefab;

    int reloadAmmo; // 총알 재장전 개수
    int currentAmmo; // 현재 총알 개수
    int maxAmmo; // 최대 소유 가능 총알 개수
    int carryAmmo; // 현재 소유 총알 개수
    float reloadTime; // 재장전 속도 -?

    TMPro.TextMeshPro text; // 총알 개수 나타내는 UI
    AudioClip fireSound; // 총알 발사 소리


    private void Start()
    {
        //Manager.Pool.CreatePool(hitEffectPrefab, 5, 5);  // 현재 씬 스크립트에 있음
    }

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

                //ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));  // 총알자국 파티클
                PooledObject hitEffect = Manager.Pool.GetPool(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffect.transform.parent = hitInfo.transform;

                Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    Debug.Log(rigid);
                    rigid.AddForceAtPosition(-hitInfo.normal * 100f, hitInfo.point, ForceMode.Impulse);  // 넉백
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

    // 사격 쿨타임 코루틴
    IEnumerator ShootCoolTimeRoutine()
    {
        isShootable = false;
        yield return new WaitForSeconds(shootCoolTime);
        isShootable = true;
    }

    // 연사 코루틴
    IEnumerator ContinuousFireRoutine()
    {
        while(isShootable)
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private Coroutine routine;

    public void ContinuousFireStart()
    {
        routine = StartCoroutine(ContinuousFireRoutine());
    }

    public void ContinuousFireStop()
    {
        StopCoroutine(routine);
    }

    public void Reload()
    {
        //currentAmmo = maxAmmo;
    }

}
