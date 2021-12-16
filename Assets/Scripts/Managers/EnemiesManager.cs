using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Entities;
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
            enemy.data = enemiesData[selectedEnemy];
            enemies.Add(enemy);
        }

        public void SortEnemiesByDistance()
        {
            //TODO: Sort enemies by distance to improve the way towers aim
        }
        public void Start()
        {
            
            enemies = new List<Enemy>();
            spawnSpot = LevelController.Instance.startPoint.transform.position + Vector3.up;
            StartCoroutine(WaveRunner());
        }

        private IEnumerator WaveRunner()
        {
            yield return new WaitForSeconds(3);
            while (true)
            {
                SpawnEnemy(0); //TODO grab data about what and how many enemies to spawn from level scriptable object 
                yield return new WaitForSeconds(3); //TODO set timer variable that can be changed with upgrade
            }
        }

        public void Update()
        {
            
        }
    }
}