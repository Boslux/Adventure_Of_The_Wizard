using System;
using System.Collections.Generic;
using UnityEngine;

namespace NRPG.Save
{
    [Serializable]

    public class SaveData
    {
        public CharacterData characterData;
        public List<EnemyData> enemies = new List<EnemyData>();   // Düşmanlar için veri listesi
        //public InventoryData inventoryData;
        //boss ve görevler için daha sonra ayarla
    }
}

[Serializable]
public class EnemyData
{
    public string enemyID;         // Benzersiz düşman kimliği
    public Vector3 position;       // Düşmanın pozisyonu
    public bool isAlive;           // Düşmanın canlı olup olmadığı
}
