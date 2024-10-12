using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackItem = 10;
    public InventorySlot[] inventorySlot;
    public GameObject inventoryItemPrefab;

    // Bu metod, belirli bir item'i envantere ekler.
    public bool PickUpItem(Item item)
    {
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            InventorySlot slot = inventorySlot[i];
            InventoryItem itemsInSlot = slot.GetComponentInChildren<InventoryItem>();

            // Eğer slot doluysa ve item aynıysa, sayıyı arttırıyoruz.
            if (itemsInSlot != null && itemsInSlot.item == item && itemsInSlot.count < maxStackItem && itemsInSlot.item.stackable)
            {
                itemsInSlot.count++;
                itemsInSlot.RefresCount();
                return true;
            }
        }

        // Eğer stacklenemiyorsa veya boş bir slot bulursak yeni item spawn ediyoruz.
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            InventorySlot slot = inventorySlot[i];
            InventoryItem itemsInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemsInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        Debug.Log("Envanter dolu, item eklenemedi.");
        return false;
    }


    // Yeni item'ı slot'a spawn eden metod.
    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();

        if (inventoryItem != null)
        {
            inventoryItem.InitialisedItem(item); // Item'i InventoryItem'e atıyoruz.
        }
        else
        {
            Debug.LogError("InventoryItem bileşeni bulunamadı.");
        }

        // Slot'a item'i ekliyoruz.
        slot.AddItem(item);
    }


    public void UseItem(Item item)
    {
        if (item.type == Itemtype.Useable)
        {
            //can, mana vs için olan kodlar
            Debug.Log("cam dolduruldu");
        }
        else if (item.type == Itemtype.Weapon)
        {
            //silah itemini giyme
            Debug.Log("zırh donanıldı");
            //itemleri farklı dosyalara aldık onları kodla
        }
        else if (item.type == Itemtype.Wearable)
        {
            //zırh vs bunları giyme işlemi
            Debug.Log("kolya takıldı");
        }
    }

    // Kullanılan item'i envanterden kaldırmak için (örneğin tek kullanımlı item'ler için)
    public void RemoveItem(Item item)
    {
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            InventorySlot slot = inventorySlot[i];
            InventoryItem itemsInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemsInSlot != null && itemsInSlot.item == item)
            {
                Destroy(itemsInSlot.gameObject); // Item'i UI'dan kaldır
                Debug.Log(item.name + " envanterden kaldırıldı.");
                return;
            }
        }
    }
}
