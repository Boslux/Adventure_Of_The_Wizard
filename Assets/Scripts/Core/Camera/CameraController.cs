using UnityEngine;

namespace NRPG.Core
{
    public class CameraController : MonoBehaviour
    {
        Transform target;
        private void Awake()
        {
            target = GameObject.Find("Player").GetComponent<Transform>();
        }
        void Update()
        {
            transform.position = target.position;
        }
    }
}
