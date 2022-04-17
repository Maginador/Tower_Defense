using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Upgrade", order = 1)]
    public class UpgradeData : ScriptableObject
    {
        public string upgradeName;
        public int scPrice, hcPrice;
        public int timeToUpgrade;
        public int tier;
        public int maxLevel; 

    }
}