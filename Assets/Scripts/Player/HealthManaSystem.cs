using UnityEngine;

public class HealthManaSystem : MonoBehaviour
{
    public bool UseMana(float manaCost, PlayerStats stats)
    {
        // Eğer yeterli mana varsa
        if (stats.currentMana >= manaCost)
        {
            // Mana'yı azalt
            stats.currentMana -= manaCost;

            // Mana'yı sınırla (eksi değerlere düşmesin)
            if (stats.currentMana < 0)
            {
                stats.currentMana = 0;
            }

            Debug.Log("Mana used: " + manaCost + " | Remaining Mana: " + stats.currentMana);
            return true; // Mana kullanım başarılı
        }
        else
        {
            Debug.LogWarning("Not enough mana! Required: " + manaCost + " | Current Mana: " + stats.currentMana);
            return false; // Yeterli mana yok
        }
    }
    public void FillManaAndHealth(PlayerStats stats)
    {
        if (stats.currentMana < stats.maxMana)
        {
            stats.currentMana += Time.deltaTime * stats.manaRegeneration;
        }
        if (stats.currentHealth < stats.maxHealth)
        {
            stats.currentHealth += Time.deltaTime * stats.healthRegeneration;
        }
    }
}