using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{

    public Camera cam;
    public GameObject buildUI;

    public void Update()
    {
        MouseAction();
    }
    public void SetTower()
    {
        
    }

    public void MouseAction()
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
        buildUI.SetActive(false);
    }

    private void ShowBuildUI(Transform hitTransform)
    {
        buildUI.transform.position = hitTransform.position;
        buildUI.SetActive(true);
    }
}
