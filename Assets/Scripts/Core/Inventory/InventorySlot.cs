using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NRPG.Core
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        // Eşya bırakıldığında çağrılır.
        public void OnDrop(PointerEventData eventData)
        {
            InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

            if (draggedItem != null)
            {
                // Eğer slot boşsa, sürüklenen eşyayı bu slota yerleştir.
                if (transform.childCount == 0)
                {
                    // parentAfterDrag güncelleniyor - bu slot sürüklenen item'in yeni parent'ı olacak.
                    draggedItem.parentAfterDrag = transform;
                    draggedItem.transform.SetParent(transform); // Bu slota yerleştir.
                    draggedItem.transform.localPosition = Vector3.zero; // Pozisyonu sıfırla.
                }
                else
                {
                    // Slot doluysa, sürüklenen eşya ile mevcut eşyayı yer değiştir.
                    InventoryItem currentItem = transform.GetChild(0).GetComponent<InventoryItem>();

                    // Mevcut item'i eski slot'a geri taşı
                    currentItem.transform.SetParent(draggedItem.parentAfterDrag);
                    currentItem.transform.localPosition = Vector3.zero;

                    // Sürüklenen item'i yeni slot'a yerleştir.
                    draggedItem.parentAfterDrag = transform; // Yeni slot parentAfterDrag olarak ayarlanıyor.
                    draggedItem.transform.SetParent(transform);
                    draggedItem.transform.localPosition = Vector3.zero;
                }
            }
        }

        // InventorySlot'a item ekleyen metod
        public void AddItem(Item item)
        {
            InventoryItem inventoryItem = GetComponentInChildren<InventoryItem>();
            if (inventoryItem != null)
            {
                inventoryItem.InitialisedItem(item); // InventoryItem'e item'i atıyoruz.
            }
        }
    }
}
