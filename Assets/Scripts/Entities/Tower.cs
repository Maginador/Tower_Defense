using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using ScriptableObjects;
using UnityEngine;

namespace Entities
{
    public class Tower : MonoBehaviour
    {

        [SerializeField] private Transform towerHead;
        [SerializeField] private Transform bulletSpawnSpot;
        [field: SerializeField] public TowerData Data { get; set; }
        [SerializeField] private GameObject rangeMeter;
        [SerializeField] private float timer;
        [SerializeField] private int level=1;
        [SerializeField] private Color[] colorUpgradeTable;
        [field: SerializeField] public int UpgradeCost { get; private set; }
        [field: SerializeField] public float UpgradeTimer { get; private set; }

        private List<GameObject> _targetList;
        private bool _towerUpgrading = false;
        private Coroutine _coroutine;
       

        public void Update()
        {
            if (_towerUpgrading)
            {
                LookUp();
                return;
                
            }
            ValidateTarget();
            LookAt();
            if (timer <= Time.time)
            {
                Shoot();
                timer = Time.time + Data.attackDelay;
            }
        }

        public void Awake()
        {
            _targetList = new List<GameObject>();
        }

        public void Start()
        {
            UpgradeCost = Mathf.FloorToInt(Data.upgradeCostMultiplier * level * Data.initialCost);
        }

        private void Shoot()
        {
            if (_targetList.Count > 0)
            {
               var bullet = Instantiate(Data.projectile,bulletSpawnSpot.position,towerHead.rotation).GetComponent<Bullet>();
               bullet.power = Mathf.FloorToInt(Data.attackPower * level * Data.upgradeStatsMultiplier);
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
            _coroutine = StartCoroutine(UpgradeDelay());
        }

        private IEnumerator UpgradeDelay()
        {
            _towerUpgrading = true;
            var time = level * 5;
            UpgradeTimer = Time.time + time;
            yield return new WaitForSeconds(time); //TODO add parameter to change easier and make it permanently upgradeable
            _towerUpgrading = false;

            DoUpgrade();
        }

        private void DoUpgrade()
        {
            level++;
            ChangeVisual();
            UpgradeCost = Mathf.FloorToInt(Data.upgradeCostMultiplier * level * Data.initialCost);        }

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

        public bool IsUpgrading()
        {
            return _towerUpgrading;
        }

        public void SpeedUp()
        {
            _towerUpgrading = false;
            StopCoroutine(_coroutine);
            DoUpgrade();
        }
    }
}
