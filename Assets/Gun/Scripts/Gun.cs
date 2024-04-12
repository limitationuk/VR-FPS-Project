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
    [Tooltip("��� ��Ÿ��")]
    [SerializeField] float shootCoolTime;
    [Tooltip("���� �ӵ�")]
    [SerializeField] float fireRate;

    [Space(20)]    
    [Header("����Ʈ / �����")]

    [Tooltip("�ѱ� ����")]
    [SerializeField] ParticleSystem muzzleFlash;
    [Tooltip("�ǰ� ��ġ ����Ʈ")]
    [SerializeField] PooledObject hitEffectPrefab;

    int reloadAmmo; // �Ѿ� ������ ����
    int currentAmmo; // ���� �Ѿ� ����
    int maxAmmo; // �ִ� ���� ���� �Ѿ� ����
    int carryAmmo; // ���� ���� �Ѿ� ����
    float reloadTime; // ������ �ӵ� -?

    TMPro.TextMeshPro text; // �Ѿ� ���� ��Ÿ���� UI
    AudioClip fireSound; // �Ѿ� �߻� �Ҹ�


    private void Start()
    {
        //Manager.Pool.CreatePool(hitEffectPrefab, 5, 5);  // ���� �� ��ũ��Ʈ�� ����
    }

    public void Fire()
    {
        if (isShootable)
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

                //ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));  // �Ѿ��ڱ� ��ƼŬ
                PooledObject hitEffect = Manager.Pool.GetPool(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffect.transform.parent = hitInfo.transform;

                Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    Debug.Log(rigid);
                    rigid.AddForceAtPosition(-hitInfo.normal * 100f, hitInfo.point, ForceMode.Impulse);  // �˹�
                }
            }
            else
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);
                Debug.Log("�� ����");
            }

            StartCoroutine(ShootCoolTimeRoutine());
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
