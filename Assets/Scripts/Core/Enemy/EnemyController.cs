using NRPG.Controller;
using NRPG.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace NRPG.Core
{
    public class EnemyController : MonoBehaviour
    {

        [Header("Enemy ID")]
        public string enemyID;  // Düşman için benzersiz ID


        [Header("Components")]
        EnemyAI _enemyAI;
        Animator _anim;
        AudioSource _audio;




        [Header("EnemyStats")]
        public bool isAlive = true;
        [SerializeField] private float _health = 10; // default 10
        [SerializeField] private AudioClip damageSound; // Hasar alındığında çalınacak ses

        //
        void Components()
        {
            _enemyAI = GetComponent<EnemyAI>();
            _anim = GetComponent<Animator>();
            _audio = GetComponent<AudioSource>();
        }

        private void Awake()
        {
            Components();
            GenerateUniqueID();

            // Eğer damageSound atanmışsa, AudioSource'a bu sesi yükleyelim
            if (damageSound != null)
            {
                _audio.clip = damageSound;
            }
        }
        private void GenerateUniqueID()
        {
            // GUID (Global Unique Identifier) ile benzersiz bir ID oluştur
            enemyID = System.Guid.NewGuid().ToString();
            Debug.Log("Generated ID for " + gameObject.name + ": " + enemyID);
        }

        void Update()
        {
            if (!isAlive)
            {
                _enemyAI.StopMoving();
            }
            else
            {
                _enemyAI.EnemyBehavior();
            }
        }

        public void TakeDamage(float damage)
        {
            // Hasar aldığında ses çalsın
            if (_audio != null && damageSound != null)
            {
                _audio.Play();
            }

            _health -= damage;

            // Hasar alma animasyonu
            _anim.SetTrigger("hurt");

            // Sağlık 0'ın altına düştüğünde düşman ölsün
            if (_health <= 0)
            {
                Died();
            }
        }

        private void Died()
        {   
            PlayerController playerController=GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.Level();
            

            float _bodyDestroyTimer = 10f;
            _health = 0;
            isAlive = false;

            // Ölüm animasyonu oynatabiliriz
            _enemyAI.Die();

            // Collider'ı devre dışı bırak
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            // Loot sistemini çalıştır
            GetComponent<LootBag>().DropLoot(new Vector2(transform.position.x+Random.Range(-3,3),transform.position.y+Random.Range(-3,3)));

            // Ölüm animasyonu sonrası yok et
            Destroy(gameObject, _bodyDestroyTimer);

            // isLive durumunu animasyonla bağlantılandır
            _anim.SetBool("isLive", isAlive);
        }


    }
}
