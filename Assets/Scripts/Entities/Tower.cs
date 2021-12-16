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
    public float timer;
    public void Update()
    {
        LookAt();
        if (timer <= Time.time)
        {
            Shoot();
            timer = Time.time + data.attackDelay;
        }
    }

    public void Shoot()
    {
        if (EnemiesManager.enemies.Count > 0)
        {
            Instantiate(data.projectile,bulletSpawnSpot.position,towerHead.rotation);
        }
    }

    public void LookAt()
    {
        if (EnemiesManager.enemies.Count > 0)
        {
            if(towerHead)
                towerHead.LookAt(EnemiesManager.enemies[0].transform);
        }
    }

    public void Upgrade()
    {
        
    }
}
