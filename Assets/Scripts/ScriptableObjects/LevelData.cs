using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Level", order = 1)]
    public class LevelData : ScriptableObject
    {

        public int waves;
        public int[] enemiesPerWave;
        public int[] enemiesInLevel;
        public int midBosses;
        public int boss;

    }
}