using UnityEngine;

namespace NRPG.Player
{
    public class PlayerSounds : MonoBehaviour
    {
        [Header("Audio Source")]
        AudioSource _audioSource;
        public AudioClip takeDamageSound;
        public AudioClip attackSound;
        public AudioClip levelUpSound;

        [Header("Footsteps")]
        public AudioSource _stepAudioSource;
        public AudioClip footstepSound;   // Ayak sesi AudioClip
        public float footstepInterval = 0.5f;  // Ayak seslerinin arasındaki süre (saniye)

        private float footstepTimer;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        public void PlaySound(int index)
        {
            switch (index)
            {
                case 0:
                    if (attackSound != null)
                        _audioSource.PlayOneShot(attackSound); // Ses klibini oynat
                    else
                        Debug.LogWarning("Attack sound is not assigned!");
                    break;

                case 1:
                    if (takeDamageSound != null)
                        _audioSource.PlayOneShot(takeDamageSound); // Ses klibini oynat
                    else
                        Debug.LogWarning("Take Damage sound is not assigned!");
                    break;

                case 2:
                    if (levelUpSound != null)
                        _audioSource.PlayOneShot(levelUpSound); // Ses klibini oynat
                    else
                        Debug.LogWarning("Level Up sound is not assigned!");
                    break;

                default:
                    Debug.LogWarning("Invalid sound index!");
                    break;
            }
        }
        public void PlayFootstepSound(Vector2 mover)
        {
            if (mover != Vector2.zero)
            {
                // Ayak sesi çalma zamanlayıcısını kontrol et
                footstepTimer -= Time.deltaTime;
                if (footstepTimer <= 0f)
                {
                    _stepAudioSource.PlayOneShot(footstepSound);  // Ayak sesini çal
                    footstepTimer = footstepInterval;  // Zamanlayıcıyı sıfırla
                }
            }
        }
    }
}