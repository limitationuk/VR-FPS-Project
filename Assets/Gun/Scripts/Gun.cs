using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // źâ ��, ������ �ð�, ���� �ӵ�, ������ Ŀ����

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
            shootFlash.Play();  // �ѱ� ��ƼŬ
            bool isRay = Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance);

            //hitInfo.distance  // �¾��� �� Ray �� �Ÿ�
            //hitInfo.collider.gameObject.name  // � ��ü�� �ε�������
            //hitInfo.point  // ���̰� ���� ��ġ
            //enemy?.TakeDamage(damage);  // ������ ������ �ְ� ������ ���ֱ�

            if (isRay)
            {
                Debug.DrawRay(shootPoint.position, shootPoint.forward * hitInfo.distance, Color.red, 0.3f);  // Ray �׷��ֱ�

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
                    rigid.AddForceAtPosition(-hitInfo.normal * 1f, hitInfo.point, ForceMode.Impulse);  // �˹�
                }
            }
            else
            {
                Debug.DrawRay(shootPoint.position, shootPoint.forward * maxDistance, Color.red, 0.3f);
                Debug.Log("�� ����");
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
