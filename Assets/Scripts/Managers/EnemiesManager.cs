using System;
using System.Collections.Generic;
using Controllers;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class EnemiesManager:MonoBehaviour
    {

        public EnemyData[] enemiesData;
        public static List<Enemy> enemies;
        private Vector3 spawnSpot;

        public void SpawnEnemy(int selectedEnemy)
        {

            var enemy = Instantiate(enemiesData[selectedEnemy].enemyPrefab,spawnSpot,Quaternion.identity).GetComponent<Enemy>();
            enemies.Add(enemy);
        }

        public void SortEnemiesByDistance()
        {
            //TODO: Sort enemies by distance to improve the way towers aim
        }
        public void Start()
        {
            enemies = new List<Enemy>();
            spawnSpot = LevelController.instance.startPoint.transform.position;
            SpawnEnemy(0);
        }

        public void Update()
        {
            
        }
    }
}