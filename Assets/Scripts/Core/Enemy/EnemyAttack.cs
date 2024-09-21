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
        private void Awake() 
        {
            _anim=GetComponent<Animator>();    
        }

        public void LongRageAttack(Vector2 target)
        {
            // Uzun mesafe saldırısı burada yer alacak
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
            Debug.Log("Saldırı gerçekleşiyor");

            // Saldırı hazırlığı
            yield return new WaitForSeconds(prepareAttack);
            if (hit.collider != null)
            {
                Debug.Log("Çarpışma algılandı");
                // Oyuncuya hasar ver
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
