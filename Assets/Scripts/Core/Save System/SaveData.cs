using System;
using System.Collections.Generic;
using UnityEngine;

namespace NRPG.Core
{
    [Serializable]

    public class SaveData
    {
        public CharacterData characterData;
        public List<EnemyData> enemies = new List<EnemyData>();   // Düşmanlar için veri listesi
        public InventoryData inventoryData; // Envanter verisi

    }
}

[Serializable]
public class EnemyData
{
    public string enemyID;         // Benzersiz düşman kimliği
    public Vector3 position;       // Düşmanın pozisyonu
    public bool isAlive;           // Düşmanın canlı olup olmadığı
}
