using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "RPG/Inventory")]
public class Inventory : ScriptableObject
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;  // Eşya adı
        public int quantity;     // Eşya sayısı
    }

    [Header("Inventory Items")]
    public List<InventoryItem> items = new List<InventoryItem>();
    public int maxSlots = 20; // Maksimum envanter kapasitesi

    // Eşya ekleme
    public void AddItem(string itemName, int quantity)
    {
        // Eşyayı envanterde bul
        InventoryItem existingItem = items.Find(item => item.itemName == itemName);
        if (existingItem != null)
        {
            // Eğer envanterde varsa, miktarı artır
            existingItem.quantity += quantity;
        }
        else
        {
            // Eğer envanterde yoksa ve yer varsa, yeni bir eşya ekle
            if (items.Count < maxSlots)
            {
                InventoryItem newItem = new InventoryItem { itemName = itemName, quantity = quantity };
                items.Add(newItem);
            }
            else
            {
                Debug.LogWarning("Inventory full!");
            }
        }
    }

    // Eşya çıkarma
    public void RemoveItem(string itemName, int quantity)
    {
        InventoryItem existingItem = items.Find(item => item.itemName == itemName);
        if (existingItem != null)
        {
            existingItem.quantity -= quantity;
            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }
        }
    }

    // Envanterdeki eşya sayısını kontrol et
    public int GetItemQuantity(string itemName)
    {
        InventoryItem item = items.Find(i => i.itemName == itemName);
        if (item != null)
            return item.quantity;
        return 0;
    }
}
