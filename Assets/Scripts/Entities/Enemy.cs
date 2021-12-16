using Controllers;
using Interfaces;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Entities
{
    public class Enemy:MonoBehaviour,IDamageable
    {
        private const float Threshold = .1f;
        public EnemyData data;
        public int health;
        public int currentPoint;
        private Vector3 _currentTarget;
        public EnemiesManager manager;

        public void Start()
        {
            health = data.health;
            currentPoint = -1;
            GetNewTarget();
        }

        public void Update()
        {
            Move();
        }

        private void GetNewTarget()
        {
            currentPoint++;
            _currentTarget = LevelController.Instance.waypoints[currentPoint].position;
            
        }

        private void Move()
        {
            if (Vector3.Distance(_currentTarget, transform.position) < Threshold)
            {
                if(currentPoint <= LevelController.Instance.waypoints.Count-2)
                {
                    GetNewTarget();
                }
                else
                {
                    Attack();
                }
            }
            else
            {
                var dir = (_currentTarget - transform.position).normalized;
                transform.Translate(dir * (data.speed * Time.deltaTime),Space.World);
            }
        }

        private void Attack()
        {
            PlayerData.Instance.baseEntity.TakeDamage(data.attack); //TODO look for close elements instead of directly the base to make it possible to have blockers
        }

        public void TakeDamage(int damage )
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public void RecoverHealth(int recover)
        {
            health += recover;
        }

        public void Die()
        {
            //TODO Add animations and effects
            manager.RemoveEnemyFromList(this);
            PlayerData.Instance.GiveGold(data.goldReward);
            PlayerData.Instance.AddExperience(data.xpReward);
            Destroy(gameObject);

        }
    }
}