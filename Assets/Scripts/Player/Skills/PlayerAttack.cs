using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRPG.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Animator")]
        Animator _anim;
        [Header("Fireball")]
        public GameObject fireball;
        Transform spawnLocation;
        [SerializeField] float fireballSpeed = 5f;
        
        [Header("Teleport")]
        [SerializeField] GameObject teleportEffect; 

        private void Awake() 
        {
            _anim = GetComponent<Animator>();
            spawnLocation = GameObject.Find("FirePosition").GetComponent<Transform>();
        }

        // Ateş topu atma işlevi
        public void FireBallAttack()
        {
            _anim.SetTrigger("attack");
            StartCoroutine(FireBallSpawner());
        }
        bool canTeleport=true;
        public IEnumerator Teleport()
        {
            if (canTeleport)
            {
                // Eski pozisyonda bulut şeysini oluştur
                Instantiate(teleportEffect,transform.position,Quaternion.identity);
                
                // Karakteri ışınlanacak konuma götür ve bulut şeysi gönder
                gameObject.transform.position = GetMouseWorldPosition();
                Instantiate(teleportEffect,transform.position,Quaternion.identity);
            }
            
            
            canTeleport=false;
            yield return new WaitForSeconds(5);
            // Tekrar saldırabilir ol
            canTeleport=true;
        }
#region Fireball
        bool canFireball=true;
        IEnumerator FireBallSpawner()
        {
            if (canFireball)
            {
                // Mouse pozisyonunu sürekli kullanmak için ayrı bir fonksiyondan al
                Vector3 mousePosition = GetMouseWorldPosition();
                
                // Mermiyi oluştur
                GameObject projectile = Instantiate(fireball, spawnLocation.position, Quaternion.identity);

                // Yön vektörünü hesapla (mouse - başlangıç pozisyonu)
                Vector2 direction = (mousePosition - spawnLocation.position).normalized;

                // Mermiyi mouse yönüne doğru hareket ettir
                projectile.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed*Time.deltaTime*100;

                // Ateş topunun Z rotasyonunu fare yönüne göre ayarla
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            canFireball=false;
            yield return new WaitForSeconds(0.5f);
            canFireball=true;
        }
#endregion

        // Mouse pozisyonunu oyun dünyası pozisyonuna dönüştüren fonksiyon
        Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // 2D oyun olduğu için Z eksenini sıfırlıyoruz
            return mousePosition;
        }
    }
}
