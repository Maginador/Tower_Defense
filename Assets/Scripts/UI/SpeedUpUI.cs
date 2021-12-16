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
            speedUpCost.text = (tower.upgradeCost * tower.data.speedUpMultiplier).ToString();
            t = tower;
        }

        public void Update()
        {
            
            var timeLeft = t.upgradeTimer - Time.time;
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