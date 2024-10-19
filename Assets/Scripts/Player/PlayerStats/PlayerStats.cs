using UnityEngine;

    public class PlayerStats : MonoBehaviour
    {
        public int level = 1;
        public float currentHealth=100f;
        public float currentMana=100f;
        public float maxHealth = 100f;
        public float maxMana = 50f;
        public float attackPower = 2f;
        public float defensePower = 5f;
        public float experience = 1f;

        public int skillPoint=0;

        public float fireBallCooldown = 1;
        public float teleportCooldown = 6f;
        public float fireWaveCooldown = 5f;

        public float manaRegeneration=5f;
        public float healthRegeneration=0.5f;

        public int money = 10;

        // Dinamik hasar hesaplama metotları
        public float GetFireWaveDamage()
        {
            return attackPower + 1;
        }

        public float GetFireBallDamage()
        {
            return attackPower - 1;
        }
    }

