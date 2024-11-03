using UnityEngine;
using UnityEngine.SceneManagement;

namespace NRPG.Core
{
    public class StartExit : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }

}