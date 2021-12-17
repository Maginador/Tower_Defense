using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Base", order = 1)]
    public class TowerData : ScriptableObject
    {
        public int initialCost;
        public int attackPower, attackRange;
        public float attackDelay,upgradeCostMultiplier, upgradeStatsMultiplier, speedUpMultiplier;
        public GameObject projectile;
        public string textName;
        public GameObject prefab;
    }
}