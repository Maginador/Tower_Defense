using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Base", order = 1)]
    public class TowerData : ScriptableObject
    {
        public int initialCost, upgradeMultiplier;
        public int attackPower, attackRange;
        public float attackDelay;
        public GameObject projectile;
        public string textName;
        public GameObject prefab;
    }
}