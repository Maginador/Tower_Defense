using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{

    [SerializeField]
    private Text goldUI, xpUI, lvlUI;

    public void Start()
    {
        PlayerData.Instance.AddOnGoldChangedListener(UpdateGoldUI);
        PlayerData.Instance.AddOnXPChangedListener(UpdateXpUI);
        UpdateGoldUI();
        UpdateXpUI();

    }

    public void UpdateGoldUI()
    {
        goldUI.text = PlayerData.Instance.CurrentGoldAmount().ToString();
    }
    
    public void UpdateXpUI()
    {
        xpUI.text = PlayerData.Instance.CurrentXp().ToString();
        lvlUI.text = PlayerData.Instance.CurrentLvl().ToString();    }
}
