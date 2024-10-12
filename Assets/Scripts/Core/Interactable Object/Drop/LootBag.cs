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

        // Bu metodla rastgele bir item seçiyoruz ve o item'in düşmesini sağlıyoruz.
        public void DropLoot(Vector3 spawnPosition)
        {
            Item droppedItem = GetDropedItem(); // Eşyanın düşmesini sağlayan metod.

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
                Debug.LogError("Hiçbir item düşmedi.");
            }
        }

        // Rastgele bir item seçen metod.
        private Item GetDropedItem()
        {
            if (lootList == null || lootList.Count == 0)
            {
                Debug.LogError("Loot listesi boş.");
                return null;
            }

            float totalDropChance = 0f;
            foreach (Loot loot in lootList)
            {
                totalDropChance += loot.dropChance;
            }

            float randomValue = Random.Range(0f, totalDropChance);
            float cumulativeChance = 0f;

            foreach (Loot loot in lootList)
            {
                cumulativeChance += loot.dropChance;
                if (randomValue <= cumulativeChance)
                {
                    return loot.item;
                }
            }
            return null;
        }
    }

    // Loot class'ı, düşebilecek olan item'leri tanımlamak için kullanılıyor.
    [System.Serializable]
    public class Loot
    {
        public Item item;
        public Sprite lootSprite;
        public float dropChance;
    }
}
