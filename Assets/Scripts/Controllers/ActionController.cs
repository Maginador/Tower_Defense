using System.Collections;
using System.Collections.Generic;
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
        buildUI.BuildTexts(Game.PlayerData);
    }

    private void Update()
    {
        MouseAction();
    }
    public void SetTower(int tower)
    {
       var t = Instantiate(Game.PlayerData.towers[tower].prefab,towerPosition,Quaternion.identity).GetComponent<Tower>();
       t.data = Game.PlayerData.towers[tower];
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
