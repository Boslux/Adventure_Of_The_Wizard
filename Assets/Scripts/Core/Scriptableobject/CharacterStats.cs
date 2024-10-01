using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "RPG/Character Stats")]
public class CharacterStats : ScriptableObject
{
    [Header("Base Stats")]
    public int level = 1;
    public float currentHealth;
    public float currentMana;
    public float maxHealth = 100f;
    public float maxMana = 50f;
    public float attackPower = 2f;
    public float defensePower = 5f;
    public float experience = 0f;

    [Header("Skill Cooldown")]
    public float fireBallCooldown = 1;
    public float teleportCooldown = 6f;
    public float fireWaveCooldown = 5f;

    [Header("Regeneration")]
    public float manaRegeneration;
    public float healthRegeneration;

    [Header("Geçici")]
    public int money = 10;

    // Dinamik hasar hesaplama metotları
    public float GetFireWaveDamage()
    {
        return attackPower + 1;  // fireWaveDamage, attackPower'a göre hesaplanıyor
    }

    public float GetFireBallDamage()
    {
        return attackPower - 1;  // fireBallDamage, attackPower'a göre hesaplanıyor
    }
}
