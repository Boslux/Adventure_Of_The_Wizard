using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRPG.Player
{
    public class TeleportAfterEffect : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 0.4f);
        }

    }
}
