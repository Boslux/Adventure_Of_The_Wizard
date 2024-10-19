using System.Collections.Generic;
using NRPG.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerStats _playerStats;
    List<EnemyController> enemies = new List<EnemyController>();
    InventoryManager inventoryManager; // Envanter verisi

    
    public bool isPaused;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        inventoryManager=FindObjectOfType<InventoryManager>();
        // Sahnedeki düşmanları ve sandıkları bul
        enemies.AddRange(FindObjectsOfType<EnemyController>());

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveSystem.SaveGame(_playerStats, enemies,inventoryManager);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void SaveGameButton()
    {
        SaveSystem.SaveGame(_playerStats, enemies,inventoryManager);
    }
    public void LoadGameButton()
    {
        SaveSystem.LoadGame(_playerStats, enemies,inventoryManager);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PauseGame() {
        pauseMenuUI.SetActive(true);  // Duraklatma menüsünü göster
        Time.timeScale = 0f;  // Oyun zamanını durdur
        isPaused = true;
    }

    public void ResumeGame() {
        pauseMenuUI.SetActive(false);  // Duraklatma menüsünü gizle
        Time.timeScale = 1f;  // Oyun zamanını normale döndür
        isPaused = false;
    }
}
