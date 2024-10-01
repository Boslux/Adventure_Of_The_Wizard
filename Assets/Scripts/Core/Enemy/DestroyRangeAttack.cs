using System.Collections;
using System.Collections.Generic;
using NRPG.Controller;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    private void Start() 
    {
        Destroy(gameObject,7f);
    }
    private void OnTriggerEnter2D(Collider2D cls) 
    {
        if (cls.gameObject.CompareTag("Player"))
        {
            PlayerController _pl=cls.gameObject.GetComponent<PlayerController>();
            if (_pl!=null)
            {
                _pl.TakeDamage(2);
                Destroy(gameObject);
            }
        }    
    }
}
