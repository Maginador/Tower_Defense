using System.Collections.Generic;
using Controllers;
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
        private void Awake()
        {
            if (PlayerPersistentData == null)
            {
                PlayerPersistentData = GetPlayerData();
            }
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            PlayfabManager.Instance.PlayerLogin();
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

        public static void OnLevelWin(LevelStatsData levelStatsData)
        {
            //TODO Validate on Backend 
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"time",levelStatsData.ElapsedTime }, 
                {"gold", levelStatsData.TotalGold}, 
                {"enemies", levelStatsData.EnemiesKilled}, 
                {"level", levelStatsData.LevelId+1}
            };
            PlayfabManager.CallFunction("ValidateLevel",parameters);
            //TODO calculate stars 
            int stars = 0;
            var entity = PlayerData.Instance.baseEntity;
            var health = entity.GetHealth();
            if (health == PlayerPersistentData.BaseHealth)
            {
                stars++;
                Debug.Log("Star 1");
            }
            if (entity.IsUntouched())
            {
                stars++;
                Debug.Log("Star 2");

            }
            if (health > PlayerPersistentData.BaseHealth / 2)
            {
                stars++;
                Debug.Log("Star 3");

            }
            PlayerPrefs.SetInt("LevelStars_" + levelStatsData.LevelId,stars);
            var rewards = Game.CalculateReward(levelStatsData);
            var xp = Game.CalculateXp(levelStatsData);

            Game.PlayerPersistentData.GainXp(xp);
            Game.PlayerPersistentData.GainRewards(rewards);
            //TODO save information about level conclusion
            PlayerPrefs.SetInt("LevelCompleted" + levelStatsData.LevelId,1);

        }

        private static int CalculateXp(LevelStatsData levelStatsData)
        {
            return levelStatsData.LevelId * levelStatsData.EnemiesKilled; //TODO improve logic for calculation
        }

        private static int[] CalculateReward(LevelStatsData levelStatsData)
        {
            int[] rewards = new int[2];
            rewards[0] = (levelStatsData.EnemiesKilled*levelStatsData.LevelId)*10; //Soft Currency Reward TODO increase reward for first time 
            rewards[1] = 1; //Hard Currency Reward TODO Request from backend

            return rewards;
        }

        public static void OnLevelLose()
        {
            
        }
    }
}