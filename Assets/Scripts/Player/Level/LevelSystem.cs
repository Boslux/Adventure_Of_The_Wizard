using System;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public void IncXp(PlayerSounds sounds, PlayerStats stats)
    {
        stats.experience+=15;
        if (stats.experience>=100)
        {
            LevelUp(sounds, stats);
            stats.experience=0;
        }
    }
    public void LevelUp(PlayerSounds sounds, PlayerStats stats)
    {
        sounds.PlaySound(2);
        // burada level atlama sesei
        stats.level++;
        stats.skillPoint++;
        // Seviye atlayınca canı tam dolsun
        stats.currentHealth = stats.maxHealth;
        Debug.Log("Level Up! New Level: " + stats.level);
    }
}