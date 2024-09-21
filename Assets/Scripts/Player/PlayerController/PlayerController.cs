using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRPG.Movement;
using NRPG.Animations;
using NRPG.Core;
using NRPG.Attack;

namespace NRPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Mover")]
        Mover mover;
        [Header("CharacterStats")]
        CharacterStats _stats;
        [Header("PlayerAttacks")]
        PlayerAttack _playerAttack;

        [Header("Layers")]
        public LayerMask walkableLayer; // Yürünebilir alanlar için LayerMask
        public LayerMask othersLayer; // diğer alanlar için LayerMask
        public LayerMask enemyLayer; // düşman alanları için LayerMask

        void Components()
        {
            mover = GetComponent<Mover>();
            _playerAttack = GetComponent<PlayerAttack>();
            _stats = Resources.Load<CharacterStats>("Character Stats");
        }

        private void Awake() 
        {
            Components();
            
        }

        private void Update() 
        {
            // Yürüme animasyonu kontrolü
            UseSkills();
            mover.IsWalking();

            if (InteractWithOthers())
            {
                return; // Eğer bu değer doğruysa, hareket kodlarına devam etmeyecek sadece combat kodunu döndürecek
            }
            if (InteractWithMovement())
            {
                return;
            }
            Debug.Log("All Functions are False");
        }

#region Movement
        private bool InteractWithMovement()
        {
            // 2D ray'i sadece "walkable" katmanına karşı fırlat
            RaycastHit2D hit = Physics2D.Raycast(MouseLocation().origin, MouseLocation().direction, Mathf.Infinity, walkableLayer);

            // Eğer bir çarpışma olursa ve walkable katmanına çarptıysa
            if (hit.collider != null)
            {
                // Sol fare tuşuna tıklama durumu
                if (Input.GetMouseButton(0))
                {
                    mover.MoveToTarget(hit.point);
                }
                return true;
            }
            return false;
        }
#endregion

#region Interaction
        bool InteractWithOthers()
        {
            RaycastHit2D hit = Physics2D.Raycast(MouseLocation().origin, MouseLocation().direction,Mathf.Infinity, othersLayer);

            if (hit.collider != null)
            {
                // Mouse objenin üstüne geldiğinde objenin üstünde ismi yazsın (eklenecek)
                if (Input.GetMouseButtonDown(0))
                {
                    IInteraction interaction = hit.collider.GetComponent<IInteraction>();
                    // İçindeki etkileşim fonksiyonunu çalıştır
                    if (interaction != null)
                    {
                        interaction.Interaction();
                    }
                    
                }
                return true;
            }
            return false;
        }
#endregion
        void UseSkills()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                 _playerAttack.FireBallAttack();
            }
            if (Input.GetKey(KeyCode.W)&&Input.GetMouseButtonDown(0))
            {
               StartCoroutine(_playerAttack.Teleport());
            }
        }
        private Ray MouseLocation()
        {
            // Ekrandaki fare konumunu 2D ray'e dönüştür
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

#region Health, level, Heal
        // Karakterin seviyesini artır
        public void LevelUp()
        {
            _stats.level++;
            _stats.maxHealth += _stats.healthGrowth;
            _stats.attackPower += _stats.attackGrowth;
            _stats.defensePower += _stats.defenseGrowth;

            // Seviye atlayınca canı tam dolsun
            _stats.currentHealth = _stats.maxHealth;
            Debug.Log("Level Up! New Level: " + _stats.level);
        }

        // Karakterin aldığı hasar
        public void TakeDamage(float damage)
        {
            float finalDamage = damage - _stats.defensePower;
            if (finalDamage < 0) finalDamage = 0; // Negatif hasar almamak için

            _stats.currentHealth -= finalDamage;
            if (_stats.currentHealth < 0) _stats.currentHealth = 0;

            Debug.Log("Damage Taken: " + finalDamage + " | Current Health: " + _stats.currentHealth);
        }

        // Canını doldur
        public void Heal(float amount)
        {
            _stats.currentHealth += amount;
            if (_stats.currentHealth > _stats.maxHealth)
                _stats.currentHealth = _stats.maxHealth;
            Debug.Log("Healed: " + amount + " | Current Health: " + _stats.currentHealth);
        }
#endregion
    }
}