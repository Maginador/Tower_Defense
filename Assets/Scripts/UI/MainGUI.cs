using UnityEngine;

namespace UI
{
    public class MainGUI : BaseUI
    {
        [SerializeField] private GameObject levelSelector, upgrade, store, exit;
        public enum GUIScreen { Main, Levels, Upgrade, Store, Exit};

        private GUIScreen _currentScreen;   
        public void Start()
        {
            HideAll();
        }

        public void BackToMainMenu()
        {
            switch (_currentScreen)
            {
                case GUIScreen.Main:
                    break;
                case GUIScreen.Levels:
                    levelSelector.SetActive(false);
                    break;
                case GUIScreen.Upgrade:
                    upgrade.SetActive(false);
                    break;
                case GUIScreen.Store:
                    store.SetActive(false);

                    break;
                case GUIScreen.Exit:
                    exit.SetActive(false);
                    break;
                default:
                    break;
            }
            mainMenu.SetActive(true);
            _currentScreen = GUIScreen.Main;
        }

        private void HideAll()
        {
            upgrade.SetActive(false);
            store.SetActive(false);
            exit.SetActive(false);
            levelSelector.SetActive(false);
            _currentScreen = GUIScreen.Main;
        }

        public void OpenUpgrade()
        {
            upgrade.SetActive(true);
            gameObject.SetActive(false);
            _currentScreen = GUIScreen.Upgrade;

        }

        public void OpenStore()
        {
            store.SetActive(true);
            gameObject.SetActive(false);
            _currentScreen = GUIScreen.Store;

        }

        public void OpenExit()
        {
            exit.SetActive(true);
            gameObject.SetActive(false);
            _currentScreen = GUIScreen.Exit;

        }
        public void OpenLevelSelector()
        {
            levelSelector.SetActive(true);
            gameObject.SetActive(false);
            _currentScreen = GUIScreen.Levels;

        }
    }
}
