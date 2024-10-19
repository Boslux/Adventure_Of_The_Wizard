using System.Collections;
using NRPG.Save;
using UnityEngine;

namespace NRPG.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Animator")]
        Animator _anim;

        [Header("CharacterStats")]
        PlayerStats _stats;

        [Header("Fireball")]
        public GameObject fireballPrefab;
        Transform spawnLocation;
        [SerializeField] float fireballSpeed = 5f;
        bool canFireball = true;

        [Header("FiraWave")]
        bool canFirewave=true;
        public GameObject fireWavePrefab;
        [SerializeField] float fireWaveSpeed = 5f;


        [Header("Teleport")]
        [SerializeField] GameObject teleportEffect;
        bool canTeleport = true;
        CapsuleCollider2D _collider;

        [Header("Cooldown")]
        float _fireballCooldown;
        float _fireWaveCooldown;
        float _teleportCooldown;

        private void Awake() 
        {
            Components();
            
            _fireballCooldown=_stats.fireBallCooldown;
            _fireWaveCooldown=_stats.fireWaveCooldown;
            _teleportCooldown=_stats.teleportCooldown;

        }
        void Components()
        {
            _collider=GetComponent<CapsuleCollider2D>();
            _anim = GetComponent<Animator>();
            spawnLocation = GameObject.Find("FirePosition").GetComponent<Transform>();
            _stats=GetComponent<PlayerStats>();
        }

        // Ateş topu atma işlevi
        public void FireBallAttack()
        {
            if (canFireball)
            {
                _anim.SetTrigger("attack");
                StartCoroutine(FireBallSpawner());
            }
        }
        public void FireWaveAttack()
        {
            if (canFirewave)
            {
                _anim.SetTrigger("attack");
                StartCoroutine(FireWaveSpawner());
            }
        }


#region Fireball

        IEnumerator FireBallSpawner()
        {
            // Cooldown kontrolü
            canFireball = false;

            // Mouse pozisyonunu oyun dünyası pozisyonuna dönüştür
            Vector3 mousePosition = GetMouseWorldPosition();

            // Ateş topunu spawn noktasından başlat
            GameObject fireball = Instantiate(fireballPrefab, spawnLocation.position, Quaternion.identity);

            // Yön vektörünü hesapla (mouse - başlangıç pozisyonu)
            Vector2 direction = (mousePosition - spawnLocation.position).normalized;

            // Rigidbody2D ile ateş topuna hız ver
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * fireballSpeed;
            }

            // Ateş topunun rotasyonunu fare yönüne göre ayarla
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            fireball.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Cooldown süresi
            yield return new WaitForSeconds(_fireballCooldown); // Ateş topunu tekrar atabilmek için bekleme süresi
            canFireball = true;
        }
#endregion

#region Teleport

        public IEnumerator Teleport()
        {
            if (canTeleport)
            {
                // Eski pozisyonda teleport efekti oluştur
                Instantiate(teleportEffect, transform.position, Quaternion.identity);
                _collider.enabled = false;
                // Karakteri yeni konuma ışınla
                transform.position = GetMouseWorldPosition();
                _collider.enabled = true;
                // Yeni pozisyonda teleport efekti oluştur
                Instantiate(teleportEffect, transform.position, Quaternion.identity);

                // Teleport cooldown
                canTeleport = false;
                yield return new WaitForSeconds(_teleportCooldown);
                canTeleport = true;
            }
        }
#endregion
#region FireWave
        IEnumerator FireWaveSpawner()
        {
            // Cooldown kontrolü
            canFirewave = false;

            // Mouse pozisyonunu oyun dünyası pozisyonuna dönüştür
            Vector3 mousePosition = GetMouseWorldPosition();

            // Ateş topunu spawn noktasından başlat
            GameObject fireball = Instantiate(fireWavePrefab, spawnLocation.position, Quaternion.identity);

            // Yön vektörünü hesapla (mouse - başlangıç pozisyonu)
            Vector2 direction = (mousePosition - spawnLocation.position).normalized;

            // Rigidbody2D ile ateş topuna hız ver
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * fireWaveSpeed;
            }

            // Ateş topunun rotasyonunu fare yönüne göre ayarla
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            fireball.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Cooldown süresi
            yield return new WaitForSeconds(_fireWaveCooldown); // Ateş topunu tekrar atabilmek için bekleme süresi
            canFirewave = true;
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
