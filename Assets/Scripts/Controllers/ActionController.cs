using System.Collections;
using System.Collections.Generic;
using Controllers;
using Entities;
using UI;
using UnityEngine;

public class ActionController : MonoBehaviour
{

    public Camera cam;
    public BuildUI buildUI;
    public UpgradeUI upgradeUI;
    public SpeedUpUI speedUpUI;
    
    public LayerMask mask;
    private Vector3 towerPosition;
    private GameObject lastClickedObject;
    private Tower selectedTower;
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
        var towerData = Game.PlayerPersistentData.towers[tower];
        if (PlayerData.Instance.HasEnoughGold(towerData.initialCost))
        {
            var t = Instantiate(towerData.prefab,towerPosition,Quaternion.identity).GetComponent<Tower>();
            t.data = towerData;
            PlayerData.Instance.SpendGold(towerData.initialCost);
            lastClickedObject.SetActive(false);
            HideBuildUI();
        }
      
    }

    public void UpgradeTower()
    {
        if (PlayerData.Instance.HasEnoughGold(selectedTower.upgradeCost))
        {
            selectedTower.StartUpgrade();
            PlayerData.Instance.SpendGold(selectedTower.upgradeCost);
            HideUpgradeUI();
        }
    }

    public void SpeedUp()
    {
        if (PlayerData.Instance.HasEnoughHC((int)(selectedTower.upgradeCost * selectedTower.data.speedUpMultiplier)))
        {
            
            PlayerData.Instance.SpendHC((int) (selectedTower.upgradeCost * selectedTower.data.speedUpMultiplier));
            selectedTower.SpeedUp();
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
                    towerPosition = hit.transform.position;
                    lastClickedObject = hit.transform.gameObject;
                    HideUpgradeUI();
                }else if (hit.collider.CompareTag("Tower"))
                {
                    
                    selectedTower = hit.transform.GetComponent<Tower>();
                    if (selectedTower.IsUpgrading())
                    {
                        ShowSpeedUpUI(selectedTower, hit.transform);
                    }
                    else
                    {
                        ShowUpgradeUI(selectedTower,
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
        upgradeUI.gameObject.transform.position = hitTransform.position;
        upgradeUI.BuildTexts(tower);
        upgradeUI.gameObject.SetActive(true);
    }
    private void HideUpgradeUI()
    {
        upgradeUI.gameObject.SetActive(false);
        
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
