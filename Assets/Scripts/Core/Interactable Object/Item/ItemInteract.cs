using UnityEngine;

namespace NRPG.Core
{
    public class ItemInteraction : MonoBehaviour, IInteraction
    {
        public void Interaction()
        {
            Debug.Log("Clicked");
        }
    }

}
