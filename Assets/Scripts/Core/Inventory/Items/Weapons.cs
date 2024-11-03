using UnityEngine;

namespace NRPG.Core
{
    [CreateAssetMenu(menuName = "Scriptable object/Item/Weapong")]

    public class Weapons : Item
    {
        public int attackPower;

        public float attackSpeed;
    }
}