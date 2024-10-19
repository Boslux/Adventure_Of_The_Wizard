using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D cls)
    {
        if (cls.gameObject.CompareTag("Player"))
        {
            // Aktif sahnenin indeksini alıyoruz
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Bir sonraki sahneye geçiyoruz
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
