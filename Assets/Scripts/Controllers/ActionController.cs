using Entities;
using Managers;
using UI;
using UnityEngine;

namespace Controllers
{
    public class ActionController : MonoBehaviour
    {

        public Camera cam;
        public BuildUI buildUI;
        public UpgradeInGameUI upgradeInGameUI;
        public SpeedUpUI speedUpUI;
    
        public LayerMask mask;
        private Vector3 _towerPosition;
        private GameObject _lastClickedObject;
        private Tower _selectedTower;
        private void Start()
        {
            SetupBuildUI();
        }

        private void SetupBuildUI()
        {
            buildUI.BuildTexts(Game.PlayerPersistentData);
        }

        private void Update()
        {
            MouseAction();
        }
        public void SetTower(int tower)
        {
            var towerData = Game.PlayerPersistentData.GetTower(tower);
            Debug.Log(towerData);
            if (PlayerData.Instance.HasEnoughGold(towerData.initialCost))
            {
                var t = Instantiate(towerData.prefab,_towerPosition,Quaternion.identity).GetComponent<Tower>();
                t.Data = towerData;
                PlayerData.Instance.SpendGold(towerData.initialCost);
                _lastClickedObject.SetActive(false);
                HideBuildUI();
            }
      
        }

        public void UpgradeTower()
        {
            if (PlayerData.Instance.HasEnoughGold(_selectedTower.UpgradeCost))
            {
                _selectedTower.StartUpgrade();
                PlayerData.Instance.SpendGold(_selectedTower.UpgradeCost);
                HideUpgradeUI();
            }
        }

        public void SpeedUp()
        {
            if (PlayerData.Instance.HasEnoughHc((int)(_selectedTower.UpgradeCost * _selectedTower.Data.speedUpMultiplier)))
            {
            
                PlayerData.Instance.SpendHc((int) (_selectedTower.UpgradeCost * _selectedTower.Data.speedUpMultiplier));
                _selectedTower.SpeedUp();
                HideSpeedUpUI();
            }
        }

        private void MouseAction()
        {
            var origin = cam.transform.position;
            var dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));
            RaycastHit hit = default;
            Debug.DrawRay(origin, dir);
            if(Input.GetButtonDown("Fire1")){
                if (Physics.Raycast(origin, dir, out hit,100,mask))
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider.CompareTag("TowerSpot"))
                    {
                        ShowBuildUI(hit.transform);
                        _towerPosition = hit.transform.position;
                        _lastClickedObject = hit.transform.gameObject;
                        HideUpgradeUI();
                    }else if (hit.collider.CompareTag("Tower"))
                    {
                    
                        _selectedTower = hit.transform.GetComponent<Tower>();
                        if (_selectedTower.IsUpgrading())
                        {
                            ShowSpeedUpUI(_selectedTower, hit.transform);
                        }
                        else
                        {
                            ShowUpgradeUI(_selectedTower,
                                hit.transform); //TODO look for a way to cash tower data to avoid get components
                            HideBuildUI();
                        }
                    }
                    else if (!hit.collider.CompareTag("UI"))
                    {
                        HideBuildUI();
                        HideUpgradeUI();
                        HideSpeedUpUI();
                    }
                }else
                {
                    HideBuildUI();
                    HideUpgradeUI();
                    HideSpeedUpUI();
                }
            }
        
        }

        private void ShowSpeedUpUI(Tower tower,Transform hitTransform)
        {
            speedUpUI.gameObject.transform.position = hitTransform.position;
            speedUpUI.BuildTexts(tower);
            speedUpUI.gameObject.SetActive(true);

        }

        private void ShowUpgradeUI(Tower tower, Transform hitTransform)
        {
            upgradeInGameUI.gameObject.transform.position = hitTransform.position;
            upgradeInGameUI.BuildTexts(tower);
            upgradeInGameUI.gameObject.SetActive(true);
        }
        private void HideUpgradeUI()
        {
            upgradeInGameUI.gameObject.SetActive(false);
        
        }

        private void HideBuildUI()
        {
            buildUI.gameObject.SetActive(false);
        
        } private void HideSpeedUpUI()
        {
            speedUpUI.gameObject.SetActive(false);
        
        }

        private void ShowBuildUI(Transform hitTransform)
        {
            buildUI.gameObject.transform.position = hitTransform.position;
            buildUI.gameObject.SetActive(true);
        }
    }
}
