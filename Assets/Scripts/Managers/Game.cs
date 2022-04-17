using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class Game : MonoBehaviour
    {
        //mocked persistentData 
        [SerializeField] private TowerData[] towers;
        [SerializeField] private int initialGold;
        private static LevelData _level;
        public void Awake()
        {
            if (PlayerPersistentData == null)
            {
                PlayerPersistentData = GetPlayerData();
            }
            DontDestroyOnLoad(this.gameObject);
        }

        private PlayerPersistentData GetPlayerData()
        {
            //TODO: check if there is stored persistentData to recover or create a new user 
            return new PlayerPersistentData(towers,initialGold);
        }

        public static PlayerPersistentData PlayerPersistentData { get; set; }

        public static LevelData GetCurrentLevel()
        {
            return _level;
        }
        public static void RunLevel(LevelData currentData)
        {
            _level = currentData;
            SceneManager.LoadScene("Level");
        }
    }
}