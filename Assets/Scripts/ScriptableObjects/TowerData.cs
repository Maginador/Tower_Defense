using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Tower", order = 1)]
    public class TowerData : ScriptableObject
    {
        public int initialCost, upgradeMultiplier;
        public int attackPower, attackSpeed, attackRange;
        public GameObject projectile;
        public string textName;
    }
}