using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Level", order = 1)]
    public class LevelData : ScriptableObject
    {
        public Texture2D levelMap;
        public int waves;
        public int[] enemiesPerWave;
        public EnemyData[] enemiesInLevel;
        public int midBosses;
        public int boss;
        public string description;
        public string levelName;
    }
}