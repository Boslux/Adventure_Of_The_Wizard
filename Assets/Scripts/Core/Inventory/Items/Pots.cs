using UnityEngine;

namespace NRPG.Core
{
    [CreateAssetMenu(menuName = "Scriptable object/Item/Pot")]

    public class Pots : Item
    {
        public int fillMana;
        public int fillHealth;
    }
}