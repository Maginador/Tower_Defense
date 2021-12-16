using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{

    [SerializeField]
    private Text goldUI;

    public void Start()
    {
        PlayerData.Instance.AddOnGoldChangedListener(UpdateGoldUI);
        goldUI.text = PlayerData.Instance.CurrentGoldAmount().ToString();

    }

    public void UpdateGoldUI()
    {
        goldUI.text = PlayerData.Instance.CurrentGoldAmount().ToString();
    }
}
