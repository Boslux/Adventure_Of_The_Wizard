using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRPG.Movement;
using NRPG.Animations;
using NRPG.Core;
using NRPG.Attack;
using NRPG.UI;
using NRPG.Save;
using UnityEngine.EventSystems;
using System.ComponentModel;

namespace NRPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Mover")]
        Mover _mover;

        [Header("CharacterStats")]
        PlayerStats _stats;

        [Header("SkillCoolDownController")]
        SkillCooldownController _skillCoolDown;

        [Header("Sounds")]
        PlayerSounds _sounds;

        [Header("UseMana")]
        HealthManaSystem _healthManaSystem;

        [Header("SkillController")]
        SkillController _skillController;

        [Header("Inventory Object")]
        GameObject _inventory;
        
        [Header("Stats Border")]
        GameObject _statsObject;

        [Header("Layers")]
        public LayerMask walkableLayer; // Yürünebilir alanlar için LayerMask
        public LayerMask othersLayer; // diğer alanlar için LayerMask
        public LayerMask enemyLayer; // düşman alanları için LayerMask

        void Components()
        {
            // Skill Cooldown Controller
            _skillCoolDown = GameObject.Find("UI_Controller").GetComponent<SkillCooldownController>();
            // Mover
            _mover = GetComponent<Mover>();
            // Player Stats 
            _stats = GetComponent<PlayerStats>();
            // Player Sounds
            _sounds = GetComponent<PlayerSounds>();
            // Health Mana System
            _healthManaSystem = GetComponent<HealthManaSystem>();
            // Skill Controller 
            _skillController = GetComponent<SkillController>();
            // Inventory Canvas
            _inventory=GameObject.Find("InventoryCanvas");
            _inventory.SetActive(false);

            _statsObject=GameObject.Find("Stats");
            _statsObject.SetActive(false);
        }

        private void Awake()
        {
            Components();
        }

        private void Update()
        {
            // Controller
            _sounds.PlayFootstepSound(_mover.pVelocity);
            _healthManaSystem.FillManaAndHealth(_stats);
            _skillController.UseSkills(_sounds, _stats, _healthManaSystem, _skillCoolDown,_inventory,_statsObject);
            _mover.IsWalking();

            if (!IsPointerOverUI()) //tıklama ui üstünde mi kontrol et
            {
                if (InteractWithOthers())
                {
                    return; // Eğer bu değer doğruysa, hareket kodlarına devam etmeyecek sadece combat kodunu döndürecek
                }
                if (InteractWithMovement())
                {
                    return;
                }
            }

            // Debug.Log("All Functions are False");
        }
        private Ray MouseLocation()
        {
            // Ekrandaki fare konumunu 2D ray'e dönüştür
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        #region Keycodes

        #endregion
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
            RaycastHit2D hit = Physics2D.Raycast(MouseLocation().origin, MouseLocation().direction, Mathf.Infinity, othersLayer);

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

        bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        #region Level, Take Damage
        // Karakterin seviyesini artır
        
        public void Level()
        {
            LevelSystem lvl=GetComponent<LevelSystem>();
            lvl.IncXp(_sounds,_stats);
        }
        // Karakterin aldığı hasar
        public void TakeDamage(float damage)
        {
            _sounds.PlaySound(1);
            // burada damage alma sesi
            float finalDamage = damage - _stats.defensePower;
            if (finalDamage < 0) finalDamage = 0; // Negatif hasar almamak için

            _stats.currentHealth -= finalDamage;
            if (_stats.currentHealth < 0) _stats.currentHealth = 0;

            Debug.Log("Damage Taken: " + finalDamage + " | Current Health: " + _stats.currentHealth);
        }
        #endregion
    }
}