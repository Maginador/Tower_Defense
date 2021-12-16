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

        private List<GameObject> targetList;
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

        private void Shoot()
        {
            if (targetList.Count > 0)
            {
                Instantiate(data.projectile,bulletSpawnSpot.position,towerHead.rotation);
            }
        }

        private void LookAt()
        {
            if (targetList.Count > 0)
            {
                if(towerHead && targetList.Count > 0)
                    towerHead.LookAt(targetList[0].transform);
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
