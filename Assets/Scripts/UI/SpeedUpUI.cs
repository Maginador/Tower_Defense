using System;
using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedUpUI : MonoBehaviour
    {
        public Text speedUpCost;
        public Text timer;
        public Tower t;
        public void BuildTexts(Tower tower)
        {
            speedUpCost.text = (tower.UpgradeCost * tower.Data.speedUpMultiplier).ToString();
            t = tower;
        }

        public void Update()
        {
            
            var timeLeft = t.UpgradeTimer - Time.time;
            if (timeLeft> 0)
            {
                timer.text = (int) (timeLeft / 60) + ":" + (int)(timeLeft % 60);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}