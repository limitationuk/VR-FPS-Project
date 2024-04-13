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
    // źâ ��, ������ �ð�, ���� �ӵ�, ������ Ŀ����

    [Header("�� �Ӽ�")]

    [Tooltip("�� ����")]
    [SerializeField] WeaponType weaponType;

    [Tooltip("�ѱ� ��ġ")]
    [SerializeField] Transform muzzlePoint;
    [Tooltip("�ִ� ��Ÿ�")]
    [SerializeField] float maxDistance;
    [Tooltip("���ݷ�")]
    [SerializeField] int damage;

    [Tooltip("��� ���� ����")]
    [SerializeField] bool isShootable;
    [Tooltip("��� ��Ÿ�� (Rifle�� ��� 0)")]
    [SerializeField] float shootCoolTime;
    [Tooltip("���� �ӵ� (Pistol�� ��� 0)")]
    [SerializeField] float fireRate;

    [Tooltip("źâ")]
    [SerializeField] MagazineSocket magazineSocket;

    [Tooltip("ź�� ���� ����")]
    [SerializeField] bool hasAmmo;
    //[Tooltip("�ִ� ���� ���� ź�� ����")]
    //[SerializeField] int maxAmmo;
    //[Tooltip("���� �����ϰ� �ִ� ��ü ź�� ����")]
    //[SerializeField] int carryAmmo;
    [Tooltip("ź�� ������ ����")]
    [SerializeField] int reloadAmmo;
    [Tooltip("���� �ѿ� �����ִ� ź�� ���� (���� �Ұ���)")]
    [SerializeField] int currentAmmo;
    //[Tooltip("������ �ӵ�")]
    //[SerializeField] float reloadTime;

    [Space(20)]
    [Header("UI")]

    TMPro.TextMeshPro text; // ź�� ���� ǥ�� UI

    [Space(20)]
    [Header("����Ʈ / ����")]

    [Tooltip("�ѱ� ����")]
    [SerializeField] ParticleSystem muzzleFlash;
    [Tooltip("�ǰ� ��ġ ����Ʈ")]
    [SerializeField] PooledObject PistolHitEffectPrefab;
    [SerializeField] PooledObject RifleHitEffectPrefab;

    AudioClip fireSound; // �Ѿ� �߻� �Ҹ�


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
            muzzleFlash.Play();  // �ѱ� ��ƼŬ
            bool isRay = Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance);

            //hitInfo.distance  // �¾��� �� Ray �� �Ÿ�
            //hitInfo.collider.gameObject.name  // � ��ü�� �ε�������
            //hitInfo.point  // ���̰� ���� ��ġ
            //enemy?.TakeDamage(damage);  // ������ ������ �ְ� ������ ���ֱ�    

            if (isRay)
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.red, 0.3f);  // Ray �׷��ֱ�

                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();  // �� ������Ʈ �����ͼ�

                // �Ӹ� ���� ���
                if ((1 << hitInfo.collider.gameObject.layer) == LayerMask.GetMask("Head"))  // Layer6: Head (64 == 64)
                {
                    Debug.Log("�Ӹ� ����");
                    enemy = hitInfo.collider.GetComponentInParent<Enemy>();
                    enemy.TakeDamage(enemy.Hp);  // damage�� �� HP�� �����ؼ� �ѹ��� �װ�
                }
                else if ((1 << hitInfo.collider.gameObject.layer) == LayerMask.GetMask("Body"))  // Layer7: Head (128 == 128)
                {
                    Debug.Log("�� ����");
                    enemy = hitInfo.collider.GetComponentInParent<Enemy>();
                    enemy.TakeDamage(damage);  // ���� �⺻ damage ����
                }
                Debug.Log("�� ��");

                //ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));  // �Ѿ��ڱ� ��ƼŬ
                PooledObject hitEffect = Manager.Pool.GetPool(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffect.transform.parent = hitInfo.transform;
                Debug.Log(hitInfo.transform.name);

                Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForceAtPosition(-hitInfo.normal * 0f, hitInfo.point, ForceMode.Impulse);  // �˹�
                }
            }
            else
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);
                Debug.Log("�� ����");
            }
            currentAmmo--;
            StartCoroutine(ShootCoolTimeRoutine());
        }
        else
        {
            Debug.Log("�Ѿ˾��ų� ��Ÿ�Ӵ���");
        }
        
    }

    // ��� ��Ÿ�� �ڷ�ƾ
    IEnumerator ShootCoolTimeRoutine()
    {
        isShootable = false;
        yield return new WaitForSeconds(shootCoolTime);
        isShootable = true;
    }

    // ���� �ڷ�ƾ
    IEnumerator ContinuousFireRoutine()
    {
        while (isShootable)
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private Coroutine routine;

    // ���� ����
    public void ContinuousFireStart()
    {
        CheckAmmoCount();
        routine = StartCoroutine(ContinuousFireRoutine());
    }

    // ���� ����
    public void ContinuousFireStop()
    {
        StopCoroutine(routine);
    }

    // ź�� ���� üũ
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
