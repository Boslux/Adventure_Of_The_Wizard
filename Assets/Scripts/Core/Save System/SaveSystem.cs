using System.IO;
using NRPG.Save;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/savefile.json";

    public static void SaveGame(PlayerStats playerStats)
    {
        SaveData data = new SaveData();
        data.characterData = new CharacterData();

        CopyStatsToData(playerStats,data.characterData);

        // JSON formatına çevir ve dosyaya yaz
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved to: " + savePath);
    }

    public static void LoadGame(PlayerStats playerStats)
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            CopyDataToStats(data.characterData,playerStats);

            Debug.Log("Game Loaded from: " + savePath);
        }
        else
        {
            Debug.LogWarning("Save file not found at: " + savePath);
        }
    }
#region copy stats data
    public static void CopyStatsToData(PlayerStats playerStats, CharacterData characterData)
    {
        characterData.level = playerStats.level;
        characterData.currentHealth = playerStats.currentHealth;
        characterData.currentMana = playerStats.currentMana;
        characterData.maxHealth = playerStats.maxHealth;
        characterData.maxMana = playerStats.maxMana;
        characterData.attackPower = playerStats.attackPower;
        characterData.defensePower = playerStats.defensePower;
        characterData.experience = playerStats.experience;

        characterData.fireBallCooldown = playerStats.fireBallCooldown;
        characterData.teleportCooldown = playerStats.teleportCooldown;
        characterData.fireWaveCooldown = playerStats.fireWaveCooldown;

        characterData.manaRegeneration = playerStats.manaRegeneration;
        characterData.healthRegeneration = playerStats.healthRegeneration;

        characterData.money = playerStats.money;
    }

    public static void CopyDataToStats(CharacterData characterData, PlayerStats playerStats)
    {
        playerStats.level = characterData.level;
        playerStats.currentHealth = characterData.currentHealth;
        playerStats.currentMana = characterData.currentMana;
        playerStats.maxHealth = characterData.maxHealth;
        playerStats.maxMana = characterData.maxMana;
        playerStats.attackPower = characterData.attackPower;
        playerStats.defensePower = characterData.defensePower;
        playerStats.experience = characterData.experience;

        playerStats.fireBallCooldown = characterData.fireBallCooldown;
        playerStats.teleportCooldown = characterData.teleportCooldown;
        playerStats.fireWaveCooldown = characterData.fireWaveCooldown;

        playerStats.manaRegeneration = characterData.manaRegeneration;
        playerStats.healthRegeneration = characterData.healthRegeneration;

        playerStats.money = characterData.money;
    }
#endregion
    
#region compy inventory data
    public static void CopyInventoryToData(Inventory inventory, InventoryData inventoryData)
    {

    }

    public static void CopyDataToInventory(InventoryData inventoryData, Inventory inventory)
    {

    }
#endregion
}
