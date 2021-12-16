using Controllers;
using Interfaces;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Entities
{
    public class Enemy:MonoBehaviour,IDamageable
    {
        public EnemyData data;
        public int currentWaypoint;
        public EnemiesManager manager;

        private int _health;
        private Vector3 _currentTarget;
        private float _attackTimer;
        private const float Threshold = .1f;

        public void Start()
        {
            _health = data.health;
            currentWaypoint = -1;
            GetNewTarget();
        }

        public void Update()
        {
            Move();
        }

        private void GetNewTarget()
        {
            currentWaypoint++;
            _currentTarget = LevelController.Instance.waypoints[currentWaypoint].position;
            
        }

        private void Move()
        {
            if (Vector3.Distance(_currentTarget, transform.position) < Threshold)
            {
                if(currentWaypoint <= LevelController.Instance.waypoints.Count-2)
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
            if (_attackTimer < Time.time)
            {
                _attackTimer = Time.time + 2;
                PlayerData.Instance.baseEntity.TakeDamage(data.attack); //TODO look for close elements instead of directly the base to make it possible to have blockers
            }
        }

        public void TakeDamage(int damage )
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        public void RecoverHealth(int recover)
        {
            _health += recover;
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