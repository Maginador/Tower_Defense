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

        public EnemyData[] enemiesData;
        public List<Enemy> enemies;
        private Vector3 _spawnSpot;


        public int currentWave;
        public int enemiesSpawned;
        
        public void SpawnEnemy(int selectedEnemy)
        {
            var enemy = Instantiate(enemiesData[selectedEnemy].enemyPrefab,_spawnSpot,Quaternion.identity).GetComponent<Enemy>();
            enemy.data = enemiesData[selectedEnemy];
            enemy.manager = this;
            enemies.Add(enemy);
            LevelController.Instance.SetSpawnedEnemies(enemies.Count);

        }
        
        public void SpawnEnemy(EnemyData selectedEnemy)
        {
            var enemy = Instantiate(selectedEnemy.enemyPrefab,_spawnSpot,Quaternion.identity).GetComponent<Enemy>();
            enemy.data = selectedEnemy;
            enemy.manager = this;
            enemies.Add(enemy);
            LevelController.Instance.SetSpawnedEnemies(enemies.Count);

        }

        public void SortEnemiesByDistance()
        {
            //TODO: Sort enemies by distance to improve the way towers aim
        }
        public void Start()
        {
            enemies = new List<Enemy>();
            _spawnSpot = LevelController.Instance.startPoint.transform.position + Vector3.up;
            StartCoroutine(WaveRunner());
            LevelController.Instance.SetWave(currentWave);
            LevelController.Instance.SetSpawnedEnemies(enemies.Count);
        }

        private IEnumerator WaveRunner()
        {
            
            yield return new WaitForSeconds(3);
            while (true)
            {
                if (enemiesSpawned < LevelController.Instance.data.enemiesPerWave[currentWave])
                {
                    enemiesSpawned++;
                    var randomEnemy = Random.Range(0, LevelController.Instance.data.enemiesInLevel.Length);
                    SpawnEnemy(LevelController.Instance.data.enemiesInLevel[randomEnemy]);
                    yield return new WaitForSeconds(1); //TODO set timer variable that can be changed with upgrade
                }
                else
                {
                    if (currentWave +1 >= LevelController.Instance.data.waves)
                    {
                        break;

                    }else{
                        currentWave++;
                        LevelController.Instance.SetWave(currentWave);
                        enemiesSpawned = 0;
                        yield return new WaitForSeconds(10);//TODO set timer variable that can be changed with upgrade
                    }
                }
            }
        }

        public void Update()
        {
            if (currentWave +1 >= LevelController.Instance.data.waves && enemies.Count == 0 && enemiesSpawned >= LevelController.Instance.data.enemiesPerWave[currentWave] )
            {
                LevelController.Instance.ShowWinScreen();

            }
        }

        public void RemoveEnemyFromList(Enemy enemy)
        {
            enemies.Remove(enemy);
            LevelController.Instance.SetSpawnedEnemies(enemies.Count);

        }
    }
}