using System;
using System.Collections;
using System.Collections.Generic;
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

        private List<GameObject> _targetList;
        private bool _blockTower = false;

        public int level=1;
        public Color[] colorUpgradeTable;
        public int upgradeCost;

        public void Update()
        {
            if (_blockTower)
            {
                LookUp();
                return;
                
            }
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
            _targetList = new List<GameObject>();
        }

        public void Start()
        {
            upgradeCost = Mathf.FloorToInt(data.upgradeCostMultiplier * level * data.initialCost);
        }

        private void Shoot()
        {
            if (_targetList.Count > 0)
            {
               var bullet = Instantiate(data.projectile,bulletSpawnSpot.position,towerHead.rotation).GetComponent<Bullet>();
               bullet.power = Mathf.FloorToInt(data.attackPower * level * data.upgradeStatsMultiplier);
            }
        }

        private void LookAt()
        {
            if (_targetList.Count > 0)
            {
                if(towerHead && _targetList.Count > 0)
                    try
                    {
                        towerHead.LookAt(_targetList[0].transform);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
            }
        }
        
        private void LookUp()
        {
            towerHead.LookAt(towerHead.position + Vector3.up);
        }

        

        private void ValidateTarget()
        {
            if (_targetList.Count > 0)
            {
                if (_targetList[0] == null)
                {
                    _targetList.RemoveAt(0);
                }
            }
        }
        public void StartUpgrade()
        {
            StartCoroutine(UpgradeDelay());
            
        }

        private IEnumerator UpgradeDelay()
        {
            _blockTower = true;
            yield return new WaitForSeconds(5); //TODO add parameter to change easier and make it permanently upgradeable
            _blockTower = false;

            DoUpgrade();
        }

        private void DoUpgrade()
        {
            level++;
            ChangeVisual();
            upgradeCost = Mathf.FloorToInt(data.upgradeCostMultiplier * level * data.initialCost);        }

        private void ChangeVisual()
        {
            towerHead.GetComponent<Renderer>().material.color = colorUpgradeTable[level];
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (!_targetList.Contains(other.gameObject))
                {
                    _targetList.Add(other.gameObject);
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {

            if (_targetList.Contains(other.gameObject))
            {
                _targetList.Remove(other.gameObject);
            }
        }
    }
}
