using System.Collections;
using UnityEngine;
using NRPG.Core;
using NRPG.Player;

public class SkillsEffect : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    [SerializeField] float _destroyDelay = 1f; // Yıkım gecikmesi
    [SerializeField] string skillTag; // Skill objesi için tag kullanılabilir
    PlayerStats _characterStats; // Performans için başta cache'liyoruz

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _characterStats=GameObject.Find("Player").GetComponent<PlayerStats>();


        StartCoroutine(DestroyAfterEffect());
    }

    IEnumerator DestroyAfterEffect()
    {
        // Yıkım animasyonu ve nesnenin durdurulması
        yield return new WaitForSeconds(_destroyDelay);
        _anim.SetTrigger("destroy");
        _rb.linearVelocity = Vector2.zero; // Hareketi durdur

        // Destroy işlemi animasyona göre zamanlanabilir
        if (skillTag == "FireBall")
        {
            yield return new WaitForSeconds(0.1f); // Yıkım sonrası küçük bekleme
        }
        
        Destroy(gameObject); // Nesneyi yok et
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            // Düşman hasar alma
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                // Çarpışma sonrası farklı işlemler
                if (skillTag == "FireBall")
                {
                    enemyController.TakeDamage(_characterStats.GetFireWaveDamage());
                    Destroy(gameObject); // FireBall çarpışınca yok olur
                }
                else if (skillTag == "FireWave")
                {
                    enemyController.TakeDamage(_characterStats.GetFireBallDamage());
                    Debug.Log("FireWave touched the enemy.");
                    // FireWave için ek işlemler buraya eklenebilir
                }
            }
        }
    }
}
