using NRPG.Controller;
using UnityEngine;

namespace NRPG.Core
{
    public class EnemyRangeAttack : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 7f);
        }
        private void OnTriggerEnter2D(Collider2D cls)
        {
            if (cls.gameObject.CompareTag("Player"))
            {
                PlayerController _pl = cls.gameObject.GetComponent<PlayerController>();
                if (_pl != null)
                {
                    _pl.TakeDamage(2);
                    Destroy(gameObject);
                }
            }
        }
    }
}
