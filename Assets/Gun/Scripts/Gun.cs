using System.Collections;
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

    [Header("�� ����")]
    public WeaponType weaponType;

    [Header("�� �ִ� ��Ÿ�")]
    [SerializeField] float maxDistance;

    [Header("�� ������")]
    [SerializeField] int damage;

    [Header("���) ��ݰ��� ���� / ��Ÿ��")]
    [SerializeField] bool isShootable;
    [SerializeField] float shootCoolTime;

    [Header("����) ���簡�� ���� / �ӵ�")]
    public bool isContinuousFireable;
    public float fireRate;

    [Header("źâ/ź��-----")]
    [SerializeField] GameObject magazine;
    [SerializeField] int ammoCount;

    [Space(30)]
    [Header("�� ����Ʈ")]

    [Header("�ѱ� ����")]
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("�Ѿ� ������ũ - ��ü")]

    [Header("�Ѿ� ������ũ - ���")]

    [Header("ź��")]
    [SerializeField] PooledObject hitEffectPrefab;

    // private
    [SerializeField] Transform muzzlePoint;
    //[SerializeField] ParticleSystem hitEffect;

    // ���
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

                //ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));  // �Ѿ��ڱ� ��ƼŬ (����ü ����� ��)
                PooledObject hitEffect = Manager.Pool.GetPool(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffect.transform.parent = hitInfo.transform;

                // �˹�
                Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForceAtPosition(-hitInfo.normal * 0.1f, hitInfo.point, ForceMode.Impulse);
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

    // �����Ÿ�� �ڷ�ƾ
    IEnumerator ShootCoolTimeRoutine()
    {
        isShootable = false;
        yield return new WaitForSeconds(shootCoolTime);
        isShootable = true;
    }

    // źâ, ź�� ���
}
