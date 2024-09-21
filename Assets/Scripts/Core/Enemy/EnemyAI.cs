using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NRPG.Core
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Enemy Settings")]
        public float detectionRadius = 10f;  // Oyuncuyu algılama yarıçapı
        public float stopDistance = 1.5f;    // Oyuncuya ne kadar yaklaştığında duracak
        public Transform player;             // Oyuncunun transform'u
        public float actionDistance = 1.5f;  // İşlemin tetikleneceği mesafe
        private bool isFacingRight = false; //default olarak karakter sola bakıyor varsayalım
        SpriteRenderer _spriteRenderer;



        private NavMeshAgent _navMeshAgent;
        private EnemyController _enemyController;
        private EnemyAttack _attack;
        private bool canAttack = true;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyController = GetComponent<EnemyController>();
            _attack = GetComponent<EnemyAttack>();
            _spriteRenderer=GetComponent<SpriteRenderer>();

            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
        }

        public void EnemyBehavior()
        {
            // Oyuncunun mesafesini kontrol et
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            FlipPlayer(player.position);
            // Oyuncu belirlenen algılama yarıçapının içine girerse takip et
            if (distanceToPlayer <= detectionRadius)
            {
                MoveTowardsPlayer();

                // Oyuncuya yeterince yaklaştığında işlemi tetikle
                if (distanceToPlayer <= actionDistance && canAttack)
                {
                    Attack();
                }

                // Oyuncuya çok yaklaştığında dur
                if (distanceToPlayer <= stopDistance)
                {
                    StopMoving();
                }
            }
            else
            {
                StopMoving();
            }
        }

        // Oyuncuya doğru hareket et
        private void MoveTowardsPlayer()
        {
            if (!_navMeshAgent.pathPending && _navMeshAgent.enabled) // Agent etkin mi?
            {
                _navMeshAgent.SetDestination(player.position);
                
            }
        }
        void FlipPlayer(Vector2 target)
        {
             bool shouldFaceRight = target.x > transform.position.x;

            // Yön değişikliği gerektiğinde flip işlemi yap
            if (shouldFaceRight != isFacingRight)
            {
                isFacingRight = shouldFaceRight;
                _spriteRenderer.flipX = !isFacingRight; // Sağdayken flipX false, soldayken true
            }
        }

        // Hareketi durdur
        public void StopMoving()
        {
            if (_navMeshAgent.enabled) // Agent etkinse durdur
            {
                _navMeshAgent.ResetPath();
            }
        }

        private void Attack()
        {
            _attack.TryShortRangeAttack(player.position);
            StartCoroutine(AttackCooldownRoutine());
        }

        private IEnumerator AttackCooldownRoutine()
        {
            canAttack = false;  // Saldırı sırasında saldırıyı durdur
            yield return new WaitForSeconds(_attack.attackCooldown);  // Cooldown süresi boyunca bekle
            canAttack = true;  // Cooldown süresi bittikten sonra tekrar saldırıya izin ver
        }

        private void OnDrawGizmosSelected()
        {
            // Algılama alanını görmek için gizmo çizin
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
