using System.Collections;
using NRPG.Attack;
using NRPG.Save;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace NRPG.UI
{
    public class SkillCoolDownController : MonoBehaviour
    {
        [Header("Skill Texts")]
        Text _fireBallText;      // Fireball becerisi için UI text
        Text _fireWaveText;      // Firewave becerisi için UI text
        Text _teleportText;      // Teleport becerisi için UI text

        [Header("Skill Cooldowns")]

        public float _fireBallTimer;
        public float _fireWaveTimer;
        public float _teleportTimer;

        private PlayerAttack _playerAttack;
        private PlayerStats _stats;

        private void Awake()
        {
            _fireBallText=GameObject.Find("T1").GetComponent<Text>();
            _fireWaveText=GameObject.Find("T2").GetComponent<Text>();    
            _teleportText=GameObject.Find("T3").GetComponent<Text>();

            _stats=GameObject.Find("Player").GetComponent<PlayerStats>();
            _playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();

            // Cooldown timer'larını başlat
            _fireBallTimer = 0;
            _fireWaveTimer = 0;
            _teleportTimer = 0;
        }

        private void Update()
        {
            // Her beceri için cooldown'ları güncelle
            UpdateCooldowns();
            UpdateUI();
        }

        void UpdateCooldowns()
        {
            // Eğer bir beceri kullanıldıysa cooldown süresini düşür
            if (_fireBallTimer > 0)
            {
                _fireBallTimer -= Time.deltaTime;
            }

            if (_fireWaveTimer > 0)
            {
                _fireWaveTimer -= Time.deltaTime;
            }

            if (_teleportTimer > 0)
            {
                _teleportTimer -= Time.deltaTime;
            }
        }

        void UpdateUI()
        {
            // Timer sıfırın üstündeyse kalan süreyi göster, sıfırsa hazır olduğuna dair bilgi
            _fireBallText.text = _fireBallTimer > 0 ? Mathf.CeilToInt(_fireBallTimer).ToString() : "";
            _fireWaveText.text = _fireWaveTimer > 0 ? Mathf.CeilToInt(_fireWaveTimer).ToString() : "";
            _teleportText.text = _teleportTimer > 0 ? Mathf.CeilToInt(_teleportTimer).ToString() : "";
        }

        // Beceri kullanıldığında timer'ı sıfırla
        public void UseFireball()
        {
            if (_fireBallTimer <= 0)
            {
                _playerAttack.FireBallAttack();
                _fireBallTimer = _stats.fireBallCooldown; // Timer'ı cooldown süresiyle sıfırla
            }
        }

        public void UseFirewave()
        {
            if (_fireWaveTimer <= 0)
            {
                _playerAttack.FireWaveAttack();
                _fireWaveTimer = _stats.fireWaveCooldown;
            }
        }

        public void UseTeleport()
        {
            if (_teleportTimer <= 0)
            {
                StartCoroutine(_playerAttack.Teleport());
                _teleportTimer = _stats.teleportCooldown;
            }
        }
    }
}
