using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class ActionController : MonoBehaviour
{

    public Camera cam;
    public BuildUI buildUI;
    private Vector3 towerPosition;

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
        }
      
    }

    private void MouseAction()
    {
        var origin = cam.transform.position;
        var dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));
        RaycastHit hit = default;
        if(Input.GetButtonDown("Fire1")){
            if (Physics.Raycast(origin, dir, out hit))
            {
                if (hit.collider.CompareTag("TowerSpot"))
                {
                    ShowBuildUI(hit.transform);
                    towerPosition = hit.transform.position;
                }else if (!hit.collider.CompareTag("UI"))
                {
                    HideBuildUI();
                }
            }else
            {
                HideBuildUI();
            }
        }
        
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
