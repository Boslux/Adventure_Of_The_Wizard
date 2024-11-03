using System.Collections;
using NRPG.Controller;
using UnityEngine;

namespace NRPG.Core
{
    public class EnemyAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        public float prepareAttack = 1f;       // Saldırı hazırlık süresi
        public float attackCooldown = 2f;      // Saldırı sonrası bekleme süresi
        public float shortRange = 1.5f;        // Kısa mesafe saldırı menzili
        public float longRange;
        public LayerMask layer;                // Saldırı hedefinin katmanı
        [SerializeField] int _damage = 1;      // Verilen hasar

        [Header("Components")]
        Animator _anim;

        [Header("Projectile Settings")]
        public GameObject projectilePrefab;    // Mermi prefab'i (örneğin ok veya ateş topu)
        Transform _firePoint;            // Merminin fırlatılacağı nokta
        [SerializeField] float projectileSpeed = 6.2f;  // Mermi hızını belirler
        private void Awake() 
        {
            _firePoint=gameObject.GetComponentInChildren<Transform>();
            _anim=GetComponent<Animator>();    
        }

        public void LongRangeAttack(Vector2 target)
        {
            StartCoroutine(LongRangeAttackCoroutine(target));
        }

        // Uzun menzilli saldırı coroutine
        private IEnumerator LongRangeAttackCoroutine(Vector2 target)
        {
            // Saldırı hazırlığı için bir bekleme süresi
            yield return new WaitForSeconds(prepareAttack);

            // Mermi prefab'ini fırlatma noktasında oluştur
            GameObject projectile = Instantiate(projectilePrefab, _firePoint.position, Quaternion.identity);

            // Hedefe doğru yön hesapla
            Vector2 direction = (target - (Vector2)_firePoint.position).normalized;

            // Merminin hareketini sağlamak için Rigidbody2D bileşeniyle hız ver
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
            }
            // Ateş topunun rotasyonunu fare yönüne göre ayarla
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Merminin hedefe ulaşması için bir animasyon oynat
            _anim.SetTrigger("attack");
        }

        public void TryShortRangeAttack(Vector2 target)
        {
            StartCoroutine(ShortRangeAttackCoroutine(target));
        }

        private IEnumerator ShortRangeAttackCoroutine(Vector2 target)
        {
            // Düşman ile hedef arasındaki yönü hesapla
            Vector2 direction = (target - (Vector2)transform.position).normalized;

            // Raycast fırlat (düşmandan oyuncuya doğru)
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shortRange, layer);
            Debug.Log("Saldiri gerçekleşiyor");

            // Saldırı hazırlığı
            yield return new WaitForSeconds(prepareAttack);
            if (hit.collider != null)
            {
                //Debug.Log("Çarpişma algilandi"+hit.collider.name);
                
                PlayerController player = hit.collider.GetComponent<PlayerController>();
                if (player != null)
                {
                    _anim.SetTrigger("attack");
                    player.TakeDamage(_damage);
                    Debug.Log("Player'a hasar verildi!");
                }
            }
        }
    }
}
