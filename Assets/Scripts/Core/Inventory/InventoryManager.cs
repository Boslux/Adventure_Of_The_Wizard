using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public int maxStackItem = 10;
    public InventorySlot[] inventorySlot;
    public GameObject inventoryItemPrefab;

    // Kuşanılan silah ve zırhın yerleştirileceği slotlar
    public Transform weaponSlot; // Weapon Equipment slotu
    public Transform armorSlot;  // Armor Equipment slotu

    [Header("PlayerEquipt")]
    PlayerEquipt playerEquipt;
    [Header("PlayerStats")]
    PlayerStats playerStats;
    [Header("Health Mana")]
    HealthManaSystem healthManaSystem;

    [Header("TextForStats")]
    public Text atkPowerText;
    public Text defPowerText;

    private void Awake()
    {
        Components();
    }
    private void Update()
    {
        atkPowerText.text = "ATK: " + playerStats.attackPower.ToString();
        defPowerText.text = "DEF: " + playerStats.defensePower.ToString();
    }
    void Components()
    {
        playerEquipt = GameObject.Find("Player").GetComponent<PlayerEquipt>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        healthManaSystem = GameObject.Find("Player").GetComponent<HealthManaSystem>();
    }
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
        if (item is Pots pot)
        {
            healthManaSystem.UsePots(playerStats, pot.fillMana, pot.fillHealth);

            RemoveItemFromInventory(item);
        }
        else if (item is Weapons weapon)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                playerEquipt.UnequipWeapon(playerStats);
                UnEquipItem(weaponSlot);
                ;
            }
            else
            {
                EquipItem(weapon, weaponSlot); // Silah kuşanılıyor
                playerEquipt.EquipWeapon(playerStats, weapon);
            }
        }
        else if (item is Armor armor)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                playerEquipt.UnequipArmor(playerStats);
                UnEquipItem(armorSlot);
            }
            else
            {
                EquipItem(armor, armorSlot); // Zırh kuşanılıyor
                playerEquipt.EquipArmor(playerStats, armor);
            }
        }
    }

    // Kuşanılan item'in doğru slot'a gönderilmesi
    private void EquipItem(Item item, Transform equipmentSlot)
    {
        // Yeni item'in kuşanılacağı slotun mevcut içeriğini temizleyelim
        foreach (Transform child in equipmentSlot)
        {
            Destroy(child.gameObject);
        }

        // Yeni item'i equipment slot'a ekleyelim
        GameObject newItemGo = Instantiate(inventoryItemPrefab, equipmentSlot);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();

        if (inventoryItem != null)
        {
            // Yeni item'i slot'a yerleştir
            inventoryItem.InitialisedItem(item);
            // Slot pozisyonunu sıfırlıyoruz
            newItemGo.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("InventoryItem bileşeni bulunamadı.");
        }
    }
    private void UnEquipItem(Transform equipmentSlot)
    {
        // Kuşanılan item'i equipment slot'tan kaldırmak için mevcut içeriği yok edelim
        foreach (Transform child in equipmentSlot)
        {
            Destroy(child.gameObject); // Slot'taki item'i yok eder
        }

        Debug.Log("Eşya çıkarıldı ve slot temizlendi.");
    }



    private void RemoveItemFromInventory(Item item)
    {
        // Envanterdeki slotları kontrol ediyoruz
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            InventorySlot slot = inventorySlot[i];
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();

            if (inventoryItem != null && inventoryItem.item == item)
            {
                // Sahnedeki InventoryItem'ı yok ediyoruz
                Destroy(inventoryItem.gameObject);
                Debug.Log(item.name + " envanterden kaldırıldı.");
                return;
            }
        }
    }

    public InventoryData SaveInventory()
    {
        InventoryData inventoryData = new InventoryData();

        foreach (InventorySlot slot in inventorySlot)
        {
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem != null && inventoryItem.item != null)
            {
                // Item'in adını ve sayısını kaydediyoruz
                InventoryItemData itemData = new InventoryItemData
                {
                    itemName = inventoryItem.item.itemName,
                    count = inventoryItem.count
                };
                inventoryData.items.Add(itemData);
            }
        }

        return inventoryData;
    }
    public InventorySlot FindEmptySlot()
    {
        foreach (InventorySlot slot in inventorySlot)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                return slot; // Boş slotu bulduk, geri döndürüyoruz
            }
        }
        Debug.LogWarning("Boş slot bulunamadı!");
        return null; // Boş slot yoksa null döner
    }

    public void LoadInventory(InventoryData inventoryData)
    {
        // Öncelikle mevcut envanteri temizliyoruz
        foreach (InventorySlot slot in inventorySlot)
        {
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem != null)
            {
                Destroy(inventoryItem.gameObject); // Mevcut item'ları kaldırıyoruz
            }
        }

        // Kaydedilmiş envanter item'lerini geri yüklüyoruz
        foreach (InventoryItemData itemData in inventoryData.items)
        {
            // Kaydedilen item'i Resources klasöründen yüklüyoruz
            Item loadedItem = Resources.Load<Item>("Items/" + itemData.itemName);
            if (loadedItem != null)
            {
                // Yeni item'i slot'a ekliyoruz
                InventorySlot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    SpawnNewItem(loadedItem, emptySlot);
                }
            }
            else
            {
                Debug.LogError("Item not found: " + itemData.itemName);
            }
        }
    }


}
