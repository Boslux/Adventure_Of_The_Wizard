using System.Collections.Generic;
using NRPG.Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerStats _playerStats;
    List<EnemyController> enemies = new List<EnemyController>();
    
    public bool isPaused;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        // Sahnedeki düşmanları ve sandıkları bul
        enemies.AddRange(FindObjectsOfType<EnemyController>());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveSystem.SaveGame(_playerStats, enemies);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void SaveGameButton()
    {
        SaveSystem.SaveGame(_playerStats, enemies);
    }
    public void LoadGameButton()
    {
        SaveSystem.LoadGame(_playerStats, enemies);
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
