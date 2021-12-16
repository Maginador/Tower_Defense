using System;
using Controllers;
using ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace
{
    public class Enemy:MonoBehaviour
    {
        private const float Threshold = .1f;
        public EnemyData data;
        public int health;
        public int currentPoint;
        private Vector3 currentTarget;
        
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

        public void GetNewTarget()
        {
            currentPoint++;
            currentTarget = LevelController.instance.waypoints[currentPoint].position;
            
        }
        
        public void Move()
        {
            if (Vector3.Distance(currentTarget, transform.position) < Threshold)
            {
                if(currentPoint <= LevelController.instance.waypoints.Count)
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
                var dir = (currentTarget - transform.position).normalized;
                transform.Translate(dir * data.speed * Time.deltaTime,Space.World);
            }
        }

        public void Attack()
        {
            
        }

        public void TakeDamage(int damage )
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            //TODO Add animations and effects
            Destroy(this.gameObject);
        }
    }
}