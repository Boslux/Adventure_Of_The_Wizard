using System;
using UnityEngine;

namespace NRPG.Save
{
    [Serializable]

    public class SaveData
    {
        public CharacterData characterData;
        public InventoryData inventoryData;
        //boss ve görevler için daha sonra ayarla
    }
}
