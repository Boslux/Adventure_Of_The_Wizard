using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "RPG/Character Stats")]
public class CharacterStats : ScriptableObject
{
    [Header("Base Stats")]
    public int level = 1;
    public float currentHealth;
    public float maxHealth = 100f;
    public float attackPower = 10f;
    public float defensePower = 5f;
    public float experience = 0f;

    [Header("Stat Growth")]
    public float healthGrowth = 20f;  // Seviye atladıkça max health artışı
    public float attackGrowth = 2f;   // Seviye atladıkça attack artışı
    public float defenseGrowth = 1.5f;// Seviye atladıkça defense artışı


}
