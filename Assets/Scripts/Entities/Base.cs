using Interfaces;

namespace Entities
{
    public class Base : Tower, IDamageable
    {
        private int _health;

        public void Start()
        {
            _health = Game.PlayerPersistentData.BaseHealth;
        }
        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        public void RecoverHealth(int recover)
        {
            _health += recover;
        }

        public void Die()
        {
            //TODO Game Over Screen, animations and effects
            Destroy(gameObject);
        }
    }

  
}
