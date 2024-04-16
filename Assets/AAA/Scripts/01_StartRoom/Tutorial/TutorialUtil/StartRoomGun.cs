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

            //hitInfo.collider.gameObject.name  // � ��ü�� �ε�������
            //hitInfo.point  // ���̰� ���� ��ġ

            if (isRay)
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.red, 0.3f);  // Ray �׷��ֱ�

                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();  // �� ������Ʈ �����ͼ�

                enemy?.TakeDamage(damage);  // ������ ������ �ְ� ������ ���ֱ�

                PooledObject hitEffect = Manager.Pool.GetPool(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffect.transform.parent = hitInfo.transform;

                /*Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForceAtPosition(-hitInfo.normal * 1f, hitInfo.point, ForceMode.Impulse);  // �˹�
                }*/
            }
            else
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.3f);
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
}
