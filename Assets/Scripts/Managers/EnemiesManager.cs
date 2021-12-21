using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Entities;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemiesManager:MonoBehaviour
    {

        [SerializeField] private EnemyData[] enemiesData;
        [SerializeField] private List<Enemy> enemies;
        private Vector3 _spawnSpot;


        [SerializeField] private int currentWave;
        [SerializeField] private int enemiesSpawned;
        
        public void SpawnEnemy(int selectedEnemy)
        {
            var enemy = Instantiate(enemiesData[selectedEnemy].enemyPrefab,_spawnSpot,Quaternion.identity).GetComponent<Enemy>();
            enemy.data = enemiesData[selectedEnemy];
            enemy.manager = this;
            enemies.Add(enemy);
            LevelController.Instance.SetSpawnedEnemies(enemies.Count);

        }

        private void SpawnEnemy(EnemyData selectedEnemy)
        {
            var enemy = Instantiate(selectedEnemy.enemyPrefab,_spawnSpot,Quaternion.identity).GetComponent<Enemy>();
            enemy.data = selectedEnemy;
            enemy.manager = this;
            enemies.Add(enemy);
            LevelController.Instance.stats.EnemiesKilled++;
            LevelController.Instance.SetSpawnedEnemies(enemies.Count);

        }

        public void SortEnemiesByDistance()
        {
            //TODO: Sort enemies by distance to improve the way towers aim
        }
        public void Start()
        {
            enemies = new List<Enemy>();
            _spawnSpot = LevelController.Instance.StartPoint.transform.position + Vector3.up;
            StartCoroutine(WaveRunner());
            LevelController.Instance.SetWave(currentWave);
            LevelController.Instance.SetSpawnedEnemies(enemies.Count);
        }

        private IEnumerator WaveRunner()
        {
            
            yield return new WaitForSeconds(3);
            while (true)
            {
                if (enemiesSpawned < currentWave * 5)
                {
                    enemiesSpawned++;
                    var randomEnemy = Random.Range(0, LevelController.Instance.Data.enemiesInLevel.Length);
                    SpawnEnemy(LevelController.Instance.Data.enemiesInLevel[randomEnemy]);
                    yield return new WaitForSeconds(1); //TODO set timer variable that can be changed with upgrade
                }
                else
                {
                    if (currentWave +1 > LevelController.Instance.Data.waves)
                    {
                        break;

                    }
                    currentWave++;
                    LevelController.Instance.SetWave(currentWave);
                    enemiesSpawned = 0;
                    yield return new WaitForSeconds(10);//TODO set timer variable that can be changed with upgrade
                }
            }
        }

        public void Update()
        {
            if (currentWave +1 > LevelController.Instance.Data.waves && enemies.Count == 0 && enemiesSpawned >= currentWave * 5 )
            {
                LevelController.Instance.ShowWinScreen();
                Destroy(this);

            }
        }

        public void RemoveEnemyFromList(Enemy enemy)
        {
            enemies.Remove(enemy);
            LevelController.Instance.SetSpawnedEnemies(enemies.Count);

        }
    }
}