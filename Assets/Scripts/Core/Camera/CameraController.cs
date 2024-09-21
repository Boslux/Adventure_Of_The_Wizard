using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    private void Awake() 
    {
        target=GameObject.Find("Player").GetComponent<Transform>();    
    }
    void Update()
    {
        transform.position=target.position;
    }
}
