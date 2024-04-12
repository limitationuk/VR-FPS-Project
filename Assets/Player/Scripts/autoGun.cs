using UnityEngine;

public class autoGun : MonoBehaviour
{
    // 탄창 수, 재장전 시간, 연사 속도, 데미지 커스텀

    [SerializeField] Transform shootPoint;
    [SerializeField] float maxDistance;
    [SerializeField] ParticleSystem shootFlash;
    [SerializeField] EnemyState enemyState;

    public void Fire()
    {
        shootFlash.Play();

        Vector3 direction = enemyState.Player.position - shootPoint.position;

        bool isRay = Physics.Raycast(shootPoint.position, direction, out RaycastHit hitInfo, maxDistance);

        if (isRay)
        {
            Debug.DrawRay(shootPoint.position, direction * hitInfo.distance, Color.red, 0.3f);
            if ((1 << hitInfo.collider.gameObject.layer) == LayerMask.GetMask("Player"))//삭제
            {
                Player player = hitInfo.collider.GetComponent<Player>();
                Debug.Log(player);
                player.TakeDamage();
                
            }

        }
        
    }
}
