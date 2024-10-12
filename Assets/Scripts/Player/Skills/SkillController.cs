using NRPG.UI;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public void UseSkills(PlayerSounds sounds, PlayerStats stats, HealthManaSystem healthManaSystem, SkillCooldownController skillCooldownController)
    {
        float qCost = 3;
        float wCost = 10;
        float eCost = 5;

        if (Input.GetKeyDown(KeyCode.Q) && stats.currentMana >= qCost && skillCooldownController._fireBallTimer <= 0)
        {
            sounds.PlaySound(0);
            skillCooldownController.UseFireball();
            healthManaSystem.UseMana(qCost, stats);
        }
        if (Input.GetKeyDown(KeyCode.W) && stats.currentMana >= wCost && skillCooldownController._fireWaveTimer <= 0)
        {
            sounds.PlaySound(0);
            skillCooldownController.UseFirewave();
            healthManaSystem.UseMana(wCost, stats);
        }
        if (Input.GetKey(KeyCode.E) && Input.GetMouseButtonDown(0) && stats.currentMana >= eCost && skillCooldownController._teleportTimer <= 0)
        {
            sounds.PlaySound(0);
            skillCooldownController.UseTeleport();
            healthManaSystem.UseMana(eCost, stats);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            skillCooldownController.UseLight();
        }
    }
}