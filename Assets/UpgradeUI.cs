using System.Collections;
using System.Collections.Generic;
using Entities;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Text upgrade, upgradeCost;
    public Text[] bonus;
    public Text[] bonusCost;
    public void BuildTexts(Tower tower)
    {
        upgradeCost.text = (tower.upgradeCost).ToString();
    }
}
