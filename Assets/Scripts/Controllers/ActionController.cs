using System.Collections;
using System.Collections.Generic;
using Controllers;
using Entities;
using UnityEngine;

public class ActionController : MonoBehaviour
{

    public Camera cam;
    public BuildUI buildUI;
    public UpgradeUI upgradeUI;
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
            HideUpgradeUI();
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
                    ShowUpgradeUI(selectedTower, hit.transform); //TODO look for a way to cash tower data to avoid get components
                    HideBuildUI();
                }
                else if (!hit.collider.CompareTag("UI"))
                {
                    HideBuildUI();
                    HideUpgradeUI();
                }
            }else
            {
                HideBuildUI();
                HideUpgradeUI();
            }
        }
        
    }

    private void ShowUpgradeUI(Tower component, Transform hitTransform)
    {
        upgradeUI.gameObject.transform.position = hitTransform.position;
        upgradeUI.BuildTexts(component);
        upgradeUI.gameObject.SetActive(true);
    }
    private void HideUpgradeUI()
    {
        upgradeUI.gameObject.SetActive(false);
        
    }

    private void HideBuildUI()
    {
        buildUI.gameObject.SetActive(false);
        
    }

    private void ShowBuildUI(Transform hitTransform)
    {
        buildUI.gameObject.transform.position = hitTransform.position;
        buildUI.gameObject.SetActive(true);
    }
}
