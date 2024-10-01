using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRPG.Movement;
using NRPG.Animations;
using NRPG.Core;
using NRPG.Attack;
using NRPG.UI;
using NRPG.Save;

namespace NRPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Mover")]
        Mover _mover;
        [Header("CharacterStats")]
        PlayerStats _stats;
        [Header("SkillCoolDownController")]
        SkillCoolDownController _skillCoolDown;


        [Header("Layers")]
        public LayerMask walkableLayer; // Yürünebilir alanlar için LayerMask
        public LayerMask othersLayer; // diğer alanlar için LayerMask
        public LayerMask enemyLayer; // düşman alanları için LayerMask

        void Components()
        {
            _skillCoolDown=GameObject.Find("UI_Controller").GetComponent<SkillCoolDownController>();
            _mover = GetComponent<Mover>();
            _stats=GetComponent<PlayerStats>();
        }

        private void Awake() 
        {
            Components();
            
        }

        private void Update() 
        {
            // Yürüme animasyonu kontrolü
            FillManaAndHealth();
            UseSkills();
            _mover.IsWalking();

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
        void UseSkills()
        {
            float qCost=3;
            float wCost=10;
            float eCost=5;

            if (Input.GetKeyDown(KeyCode.Q) && _stats.currentMana>=qCost && _skillCoolDown._fireBallTimer <= 0)
            {
                _skillCoolDown.UseFireball();
                UseMana(qCost);
            }
            if (Input.GetKeyDown(KeyCode.W)&&_stats.currentMana>=wCost && _skillCoolDown._fireWaveTimer <= 0)
            {
                _skillCoolDown.UseFirewave();
                UseMana(wCost);
            }
            if (Input.GetKey(KeyCode.E) && Input.GetMouseButtonDown(0) && _stats.currentMana>=eCost &&_skillCoolDown._teleportTimer <= 0)
            {
                _skillCoolDown.UseTeleport();
                UseMana(eCost);
            }

        }
        private Ray MouseLocation()
        {
            // Ekrandaki fare konumunu 2D ray'e dönüştür
            return Camera.main.ScreenPointToRay(Input.mousePosition);
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
                    _mover.MoveToTarget(hit.point);
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

#region Health, level, Heal
        // Karakterin seviyesini artır

        public void LevelUp()
        {
            _stats.level++;
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
#region Mana
        
        public bool UseMana(float manaCost)
        {
            // Eğer yeterli mana varsa
            if (_stats.currentMana >= manaCost)
            {
                // Mana'yı azalt
                _stats.currentMana -= manaCost;

                // Mana'yı sınırla (eksi değerlere düşmesin)
                if (_stats.currentMana < 0)
                {
                    _stats.currentMana = 0;
                }

                Debug.Log("Mana used: " + manaCost + " | Remaining Mana: " + _stats.currentMana);
                return true; // Mana kullanım başarılı
            }
            else
            {
                Debug.LogWarning("Not enough mana! Required: " + manaCost + " | Current Mana: " + _stats.currentMana);
                return false; // Yeterli mana yok
            }
        }
#endregion
        void FillManaAndHealth()
        {
            if (_stats.currentMana<_stats.maxMana)
            {
                _stats.currentMana+=Time.deltaTime*_stats.manaRegeneration;
            }
            if (_stats.currentHealth<_stats.maxHealth)
            {
                _stats.currentHealth+=Time.deltaTime*_stats.healthRegeneration;
            }
            
        }
        // Canını doldur
        void Heal(float amount)
        {
            _stats.currentHealth += amount;
            if (_stats.currentHealth > _stats.maxHealth)
                _stats.currentHealth = _stats.maxHealth;
            Debug.Log("Healed: " + amount + " | Current Health: " + _stats.currentHealth);
        }
        public void IncreaseMoney()
        {
            int newMoney=Random.Range(0, 10);
            _stats.money+=newMoney;
        }
#endregion
    }
}