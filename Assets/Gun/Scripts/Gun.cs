using System.Collections;
using System.ComponentModel;
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
    [Tooltip("사격 쿨타임 (Rifle일 경우 0)")]
    [SerializeField] float shootCoolTime;
    [Tooltip("연사 속도 (Pistol일 경우 0)")]
    [SerializeField] float fireRate;

    [Tooltip("탄창")]
    [SerializeField] MagazineSocket magazineSocket;

    [Tooltip("탄약 소유 여부")]
    [SerializeField] bool hasAmmo;
    //[Tooltip("최대 소유 가능 탄약 개수")]
    //[SerializeField] int maxAmmo;
    //[Tooltip("현재 소유하고 있는 전체 탄약 개수")]
    //[SerializeField] int carryAmmo;
    [Tooltip("탄약 재장전 개수")]
    [SerializeField] int reloadAmmo;
    [Tooltip("현재 총에 남아있는 탄약 개수 (수정 불가능)")]
    [SerializeField] int currentAmmo;
    //[Tooltip("재장전 속도")]
    //[SerializeField] float reloadTime;

    [Space(20)]
    [Header("UI")]

    TMPro.TextMeshPro text; // 탄약 개수 표시 UI

    [Space(20)]
    [Header("이펙트 / 사운드")]

    [Tooltip("총구 섬광")]
    [SerializeField] ParticleSystem muzzleFlash;
    [Tooltip("피격 위치 이펙트")]
    [SerializeField] PooledObject PistolHitEffectPrefab;
    [SerializeField] PooledObject RifleHitEffectPrefab;

    AudioClip fireSound; // 총알 발사 소리


    private void Start()
    {
        if (weaponType == WeaponType.Pistol)
        {
            fireRate = 0;
            RifleHitEffectPrefab = null;
        }
        else if (weaponType == WeaponType.Rifle)
        {
            shootCoolTime = 0;
            PistolHitEffectPrefab = null;
        }
        currentAmmo = reloadAmmo;
        isShootable = true;
        hasAmmo = true;
    }

    public void Fire()
    {
        CheckAmmoCount();
        if (isShootable && hasAmmo)
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
                Debug.Log("총 쏨");

                //ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));  // 총알자국 파티클
                PooledObject hitEffect = Manager.Pool.GetPool(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffect.transform.parent = hitInfo.transform;
                Debug.Log(hitInfo.transform.name);

                Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForceAtPosition(-hitInfo.normal * 0f, hitInfo.point, ForceMode.Impulse);  // 넉백
                }
            }
            else
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);
                Debug.Log("안 맞음");
            }
            currentAmmo--;
            StartCoroutine(ShootCoolTimeRoutine());
        }
        else
        {
            Debug.Log("총알없거나 쿨타임덜참");
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
        while (isShootable)
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private Coroutine routine;

    // 연사 시작
    public void ContinuousFireStart()
    {
        CheckAmmoCount();
        routine = StartCoroutine(ContinuousFireRoutine());
    }

    // 연사 중지
    public void ContinuousFireStop()
    {
        StopCoroutine(routine);
    }

    // 탄약 개수 체크
    public void CheckAmmoCount()
    {
        if (currentAmmo <= 0)
        {
            hasAmmo = false;
        }
        else
        {
            hasAmmo = true;
        }
    }

    public void Reload()
    {
        currentAmmo = reloadAmmo;
    }

}
