using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Enemies", order = 1)]
    public class EnemyData : ScriptableObject
    {
        public int health, defense, attack, speed;
        public GameObject enemyPrefab;
        public int goldReward;
        public int xpReward;
    }
}