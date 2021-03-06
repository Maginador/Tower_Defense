using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameUIController : MonoBehaviour
    {

        [SerializeField]
        private Text goldUI, xpUI, lvlUI, diamondsUI;
        [SerializeField]
        private Text waves, enemies;

        [SerializeField] private GameObject winScreen, loseScreen;

        public void Start()
        {
            PlayerData.Instance.AddOnGoldChangedListener(UpdateGoldUI);
            PlayerData.Instance.AddOnHCChangedListener(UpdateDiamondUI);
            PlayerData.Instance.AddOnXPChangedListener(UpdateXpUI);
            LevelController.Instance.AddOnWaveChangedListener(UpdateWave);
            LevelController.Instance.AddOnEnemiesQuantityChangedListener(UpdateEnemies);
            UpdateGoldUI();
            UpdateXpUI();
            UpdateEnemies();
            UpdateWave();
            UpdateDiamondUI();
        }

        public void UpdateGoldUI()
        {
            goldUI.text = PlayerData.Instance.CurrentGoldAmount().ToString();
        }
    
        public void UpdateDiamondUI()
        {
            diamondsUI.text = PlayerData.Instance.CurrentHcAmount().ToString();
        }
    
        public void UpdateXpUI()
        {
            xpUI.text = PlayerData.Instance.CurrentXp().ToString();
            lvlUI.text = PlayerData.Instance.CurrentLvl().ToString();
        
        }
    
        public void UpdateWave()
        {
            waves.text = (LevelController.Instance.CurrentWave()) + "/" + (LevelController.Instance.GetMaxWaves());
        
        }
    
        public void UpdateEnemies()
        {
            enemies.text = LevelController.Instance.CurrentQuantityOfEnemies().ToString();
        
        }

        public void ShowWinScreen()
        {
            winScreen.SetActive(true);
        
        }

        public void ShowLoseScreen()
        {
            loseScreen.SetActive(true);
        }

        public void Pause()
        {
            TimeManager.Instance.StopGame();
        }

        public void ChangeGameSpeed(int time)
        {
            switch (time)
            {
                case 1 : 
                    TimeManager.Instance.SetDefaultTime();
                    break;
                case 2 : 
                    TimeManager.Instance.Set2XTime();
                    break;
                case 3 : 
                    TimeManager.Instance.Set3XTime();
                    break;
            }
        }
    }
}
