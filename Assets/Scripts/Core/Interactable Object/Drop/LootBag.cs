    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

namespace NRPG.Core
{
    public class LootBag : MonoBehaviour
    {
        public GameObject dropedItemPrefab;
        public List<Loot> lootList=new List<Loot>();

        Loot GetDropedItem()
        {
            int randomNumber=Random.Range(1,101);
            List<Loot> possibleItems=new List<Loot>();

            foreach(Loot item in lootList)
            {
                if(randomNumber<=item.dropChance)
                {
                    possibleItems.Add(item);
                }
            }
            if (possibleItems.Count>0)
            {
                Loot droppedItem=possibleItems[Random.Range(0,possibleItems.Count)];
                return droppedItem;
            }
            Debug.Log("No droped item");
            return null;
        }
        public void InstantiateLoot(Vector3 spawnposition)
        {
            Loot dropedItem=GetDropedItem();
            if (dropedItem!=null)
            {
                GameObject lootGameObject=Instantiate(dropedItemPrefab, spawnposition,Quaternion.identity);
                lootGameObject.GetComponent<SpriteRenderer>().sprite=dropedItem.lootSprite;

                float dropForce =300f;
                Vector2 droppDirection=new Vector2(Random.Range(-1f,1f),Random.Range(-1,1f));
                lootGameObject.GetComponent<Rigidbody2D>().AddForce(droppDirection*dropForce,ForceMode2D.Impulse);
            }
        }

            
    }
}