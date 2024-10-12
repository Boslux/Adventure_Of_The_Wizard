using System.Collections.Generic;
using System.IO;
using NRPG.Core;
using NRPG.Save;
using UnityEngine;
using UnityEngine.AI;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/savefile.json";

    public static void SaveGame(PlayerStats playerStats, List<EnemyController> enemies)
    {
        SaveData data = new SaveData();
        data.characterData = new CharacterData();

        // Karakterin istatistiklerini kopyala
        CopyStatsToData(playerStats, data.characterData);

        // Düşmanların durumlarını kopyala
        foreach (EnemyController enemy in enemies)
        {
            EnemyData enemyData = new EnemyData
            {
                enemyID = enemy.enemyID,              // Benzersiz ID'yi kullan
                position = enemy.transform.position,
                isAlive = enemy.isAlive
            };
            data.enemies.Add(enemyData);
        }

        // JSON formatına çevir ve dosyaya yaz
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved to: " + savePath);
    }

    public static void LoadGame(PlayerStats playerStats, List<EnemyController> enemies)
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Karakterin istatistiklerini kopyala
            CopyDataToStats(data.characterData, playerStats);

            // Düşmanların durumlarını geri yükle
            foreach (EnemyData enemyData in data.enemies)
            {
                EnemyController enemy = enemies.Find(e => e.enemyID == enemyData.enemyID);
                if (enemy != null)
                {
                    enemy.transform.position = enemyData.position;
                    enemy.isAlive = enemyData.isAlive;
                    enemy.gameObject.SetActive(enemy.isAlive);
                }
            }

            Debug.Log("Game Loaded from: " + savePath);
        }
        else
        {
            Debug.LogWarning("Save file not found at: " + savePath);
        }
    }
    #region Stats Data
    public static void CopyStatsToData(PlayerStats playerStats, CharacterData characterData)
    {
        if (playerStats == null || characterData == null)
        {
            Debug.LogError("PlayerStats or CharacterData is null.");
            return; // Eğer referans null ise işlem yapılmadan fonksiyondan çık
        }

        // Karakterin son konumunu characterData'ya kaydet
        Transform playerTransform = GameObject.Find("Player")?.GetComponent<Transform>();
        if (playerTransform != null)
        {
            characterData.lastPosition = playerTransform.localPosition; // Konum kaydediliyor
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }

        // playerStats'tan characterData'ya veri kopyala
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
        if (characterData == null || playerStats == null)
        {
            Debug.LogError("CharacterData or PlayerStats is null.");
            return; // Eğer referans null ise işlem yapılmadan fonksiyondan çık
        }

        // Kayıtlı dosyadan karakterin son konumunu yükle
        Transform playerTransform = GameObject.Find("Player")?.GetComponent<Transform>();
        if (playerTransform != null)
        {
            playerTransform.localPosition = characterData.lastPosition; // Konum yükleniyor
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }
        // NavMeshAgent'ın hedefini sıfırla (karaktere yeni bir hedef verilmemeli)
        NavMeshAgent agent = playerTransform.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.ResetPath(); // NavMeshAgent'in hedefini sıfırlıyoruz
        }

        // characterData'dan playerStats'a veri kopyala
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
}
