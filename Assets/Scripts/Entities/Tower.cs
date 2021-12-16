using System;
using System.Collections.Generic;
using Controllers;
using ScriptableObjects;
using UnityEngine;

namespace Entities
{
    public class Tower : MonoBehaviour
    {

        public Transform towerHead;
        public Transform bulletSpawnSpot;
        public TowerData data;
        public GameObject rangeMeter;
        public float timer;

        private List<GameObject> targetList;
        public int level=1;
        public Color[] colorUpgradeTable;
        public int upgradeCost;

        public void Update()
        {
            ValidateTarget();
            LookAt();
            if (timer <= Time.time)
            {
                Shoot();
                timer = Time.time + data.attackDelay;
            }
        }

        public void Awake()
        {
            targetList = new List<GameObject>();
        }

        public void Start()
        {
            upgradeCost = Mathf.FloorToInt(data.upgradeCostMultiplier * level * data.initialCost);
        }

        private void Shoot()
        {
            if (targetList.Count > 0)
            {
               var bullet = Instantiate(data.projectile,bulletSpawnSpot.position,towerHead.rotation).GetComponent<Bullet>();
               bullet.power = Mathf.FloorToInt(data.attackPower * level * data.upgradeStatsMultiplier);
            }
        }

        private void LookAt()
        {
            if (targetList.Count > 0)
            {
                if(towerHead && targetList.Count > 0)
                    try
                    {
                        towerHead.LookAt(targetList[0].transform);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
            }
        }


        private void ValidateTarget()
        {
            if (targetList.Count > 0)
            {
                if (targetList[0] == null)
                {
                    targetList.RemoveAt(0);
                }
            }
        }
        public void Upgrade()
        {
            level++;
            ChangeVisual();
            PlayerData.Instance.SpendGold(upgradeCost);
            upgradeCost = Mathf.FloorToInt(data.upgradeCostMultiplier * level * data.initialCost);
        }

        private void ChangeVisual()
        {
            towerHead.GetComponent<Renderer>().material.color = colorUpgradeTable[level];
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (!targetList.Contains(other.gameObject))
                {
                    targetList.Add(other.gameObject);
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {

            if (targetList.Contains(other.gameObject))
            {
                targetList.Remove(other.gameObject);
            }
        }
    }
}
