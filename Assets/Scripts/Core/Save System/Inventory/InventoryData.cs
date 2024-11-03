using System.Collections.Generic;

namespace NRPG.Core
{
    [System.Serializable]
    public class InventoryData
    {
        public List<InventoryItemData> items = new List<InventoryItemData>();
    }

}