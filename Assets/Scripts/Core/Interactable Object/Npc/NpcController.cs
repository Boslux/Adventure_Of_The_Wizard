using System.Collections;
using System.Collections.Generic;
using NRPG.Core;
using UnityEngine;

namespace NRPG.Core
{
    public class NpcController : MonoBehaviour, IInteraction
    {
        public void Interaction()
        {
            Debug.Log("Interact with: " + gameObject.name);
        }
    }

}