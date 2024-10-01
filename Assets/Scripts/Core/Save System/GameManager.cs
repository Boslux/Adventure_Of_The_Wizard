using UnityEngine;
namespace NRPG.Save
{
    public class GameManager : MonoBehaviour 
    {
        PlayerStats _playerStats;
        private void Awake() 
        {
            _playerStats=GameObject.Find("Player").GetComponent<PlayerStats>();    
        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SaveSystem.SaveGame(_playerStats);
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                SaveSystem.LoadGame(_playerStats);
            }
        }    
    }
}