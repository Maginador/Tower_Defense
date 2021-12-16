using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using UnityEngine;

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
        CheckRange();
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

    public void Shoot()
    {
        if (targetList.Count > 0)
        {
            Instantiate(data.projectile,bulletSpawnSpot.position,towerHead.rotation);
        }
    }

    public void LookAt()
    {
        if (targetList.Count > 0)
        {
            if(towerHead)
                towerHead.LookAt(targetList[0].transform);
        }
    }


    public void CheckRange()
    {
        
    }
    public void Upgrade()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("TESTE1");
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
        Debug.Log("TESTE2");

        if (targetList.Contains(other.gameObject))
        {
            targetList.Remove(other.gameObject);
        }
    }
}
