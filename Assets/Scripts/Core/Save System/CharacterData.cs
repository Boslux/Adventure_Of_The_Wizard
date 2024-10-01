using System;

namespace NRPG.Save
{
    [Serializable]
    public class CharacterData
    {
        public int level;
        public float currentHealth;
        public float currentMana;
        public float maxHealth;
        public float maxMana;
        public float attackPower;
        public float defensePower;
        public float experience;

        public float fireBallCooldown;
        public float teleportCooldown;
        public float fireWaveCooldown;

        public float manaRegeneration;
        public float healthRegeneration;

        public int money;
    }
}
