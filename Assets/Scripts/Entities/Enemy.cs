using System;
using ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace
{
    public class Enemy:MonoBehaviour
    {
        public EnemyData data;
        public int health;

        public void Start()
        {
            health = data.health;
        }

        public void Move()
        {
            
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