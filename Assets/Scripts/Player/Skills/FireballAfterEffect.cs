using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireballAfterEffect : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    private void Start() 
    {
        _rb=GetComponent<Rigidbody2D>();
        _anim=GetComponent<Animator>();
        StartCoroutine(DestoryObject());
    }
    IEnumerator DestoryObject()
    {
        yield return new WaitForSeconds(0.6f);
        _anim.SetTrigger("destroy");
        _rb.velocity = Vector3.zero;
       yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

