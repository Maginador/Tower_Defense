using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeInGameUI : MonoBehaviour
    {
        public Text upgrade, upgradeCost;
        public Text[] bonus;
        public Text[] bonusCost;
        public void BuildTexts(Tower tower)
        {
            upgradeCost.text = (tower.UpgradeCost).ToString();
        }
    }
}
