using UnityEngine;

namespace NRPG.Core
{
    public class LootInteract : MonoBehaviour
    {
        private Item item; // Etkileşimde bulunan item

        // Bu metod ile item referansı atanır.
        public void Initialize(Item droppedItem)
        {
            item = droppedItem;
        }

        // Oyuncu item ile etkileşime girdiğinde çalışır.
        
        private void OnMouseDown()
        {
            // InventoryManager'ı bulup item'i envantere ekleyelim.
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null && item != null)
            {
                bool wasPickedUp = inventoryManager.PickUpItem(item);
                if (wasPickedUp)
                {
                    Destroy(gameObject); // Envantere eklendikten sonra sahneden kaldırıyoruz.
                }
            }
        }
    }
}
