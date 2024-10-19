using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRPG.Core
{
    public class LootBag : MonoBehaviour
    {
        [Tooltip("Düşen eşyaların prefab'ı.")]
        public GameObject dropedItemPrefab; // Düşen eşyanın prefab'ı.

        [Tooltip("Bu loot bag'den düşebilecek eşyaların listesi.")]
        public List<Loot> lootList = new List<Loot>(); // Düşebilecek eşyaların listesi.

        [Tooltip("Hiçbir şey düşmeme ihtimali (yüzde olarak).")]
        [Range(0, 100)] 
        public float noDropChance = 0f; // Hiçbir şey düşmeme şansı.

        // Rastgele bir item seçiyoruz ve o item'in düşmesini sağlıyoruz.
        public void DropLoot(Vector3 spawnPosition)
        {
            Item droppedItem = GetDroppedItem(); // Eşyanın düşmesini sağlayan metod.

            if (droppedItem != null)
            {
                // Yeni bir oyun objesi oluşturuluyor.
                GameObject lootGameObject = Instantiate(dropedItemPrefab, spawnPosition, Quaternion.identity);

                // SpriteRenderer ile düşen item'in görselini ayarlıyoruz.
                SpriteRenderer spriteRenderer = lootGameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = droppedItem.image;
                }

                // Burada item referansını alıyoruz, böylece etkileşimde ne olduğunu biliyoruz.
                lootGameObject.AddComponent<LootInteract>().Initialize(droppedItem);
            }
            else
            {
                Debug.Log("Hiçbir item düşmedi.");
            }
        }

        // Rastgele bir item seçen metod.
        private Item GetDroppedItem()
        {
            if (lootList == null || lootList.Count == 0)
            {
                Debug.LogError("Loot listesi boş.");
                return null;
            }

            // Tüm drop şanslarını hesaplıyoruz
            float totalDropChance = CalculateTotalDropChance();

            // Eğer toplam şans 0 veya daha azsa, hiçbir eşya düşmeyecektir.
            if (totalDropChance <= 0)
            {
                Debug.LogWarning("Toplam drop şansı sıfır veya negatif. Hiçbir şey düşmeyecek.");
                return null;
            }

            // Rastgele bir sayı seçiyoruz
            float randomValue = Random.Range(0f, totalDropChance);

            float cumulativeChance = 0f;
            foreach (Loot loot in lootList)
            {
                cumulativeChance += loot.dropChance;

                if (randomValue <= cumulativeChance)
                {
                    Debug.Log("Seçilen item: " + loot.item.name); // Debug için
                    return loot.item;
                }
            }

            // Eğer bir şey seçilemediyse (hiçbir şey düşmedi)
            return null;
        }

        // Toplam drop şansını hesaplayan metod
        private float CalculateTotalDropChance()
        {
            float totalChance = 0f;

            // Tüm item'ların drop şansını ekle
            foreach (Loot loot in lootList)
            {
                totalChance += loot.dropChance;
            }

            // No drop şansını ekle
            totalChance += noDropChance;

            return totalChance;
        }
    }

    // Loot class'ı, düşebilecek olan item'leri tanımlamak için kullanılıyor.
    [System.Serializable]
    public class Loot
    {
        public Item item;
        public float dropChance; // Eşyanın düşme şansı.
    }
}
