using NRPG.Player;
using UnityEngine;

namespace NRPG.Core
{
    public class HealthManaBarController : MonoBehaviour
    {
        Animator _healthAnim;
        Animator _manaAnim;

        PlayerStats _stats;

        private void Awake()
        {
            Components();
        }
        void Components()
        {
            _healthAnim = GameObject.Find("HealthBar").GetComponent<Animator>();
            _manaAnim = GameObject.Find("ManaBar").GetComponent<Animator>();
            _stats = GameObject.Find("Player").GetComponent<PlayerStats>();
        }
        private void Update()
        {
            UpdateManaUI(); UpdateHealthUI();
        }
        void UpdateManaUI()
        {
            _manaAnim.SetFloat("manaCount", _stats.currentMana);
        }
        void UpdateHealthUI()
        {
            _healthAnim.SetFloat("healthCount", _stats.currentHealth);
        }
    }
}
