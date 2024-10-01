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
        Transform _player;             // Oyuncunun transform'u
        public float actionDistance = 1.5f;  // İşlemin tetikleneceği mesafe
        private bool isFacingRight = false; //default olarak karakter sola bakıyor varsayalım
        bool _isWalking;
        [SerializeField] string enemyTag;

        [Header("Components")]
        SpriteRenderer _spriteRenderer;
        private NavMeshAgent _navMeshAgent;
        private EnemyAttack _attack;
        Animator _anim;

        [Header("Can Attack")]
        private bool canAttack = true;

        private void Awake()
        {
            Components();

            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
        }
        void Components()
        {
            _player=GameObject.Find("Player").GetComponent<Transform>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _attack = GetComponent<EnemyAttack>();
            _spriteRenderer=GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
        }

        public void EnemyBehavior()
        {
            // Oyuncunun mesafesini kontrol et
            float distanceToPlayer = Vector3.Distance(_player.position, transform.position);
            FlipPlayer(_player.position);
            // Oyuncu belirlenen algılama yarıçapının içine girerse takip et
            if (distanceToPlayer <= detectionRadius)
            {
                MoveTowardsPlayer();

                // Oyuncuya yeterince yaklaştığında işlemi tetikle
                if (distanceToPlayer <= actionDistance && canAttack)
                {
                    Debug.Log("1");
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
                _navMeshAgent.SetDestination(_player.position);
                IsWalk(true);
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
                IsWalk(false);
            }
        }
        public void Die()
        {
            _navMeshAgent.enabled = false;
        }

        private void Attack()
        {
            if (enemyTag=="LongRange")
            {
                _attack.LongRangeAttack(_player.position);
            }
            if (enemyTag=="ShortRange")
            {
                _attack.TryShortRangeAttack(_player.position);
            }
            StartCoroutine(AttackCooldownRoutine());
            Debug.Log("asdassadasa");
        }

        private IEnumerator AttackCooldownRoutine()
        {
            canAttack = false;  // Saldırı sırasında saldırıyı durdur
            yield return new WaitForSeconds(_attack.attackCooldown);  // Cooldown süresi boyunca bekle
            canAttack = true;  // Cooldown süresi bittikten sonra tekrar saldırıya izin ver
        }
        void IsWalk(bool isWalk)
        {
            _anim.SetBool("isWalking",_isWalking);
        }

        private void OnDrawGizmosSelected()
        {
            // Algılama alanını görmek için gizmo çizin
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
