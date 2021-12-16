using Controllers;
using Interfaces;
using UnityEngine;

namespace Entities
{
    public class Base : Tower, IDamageable
    {
        private int _health, _timeToRecover, _constHealthRecover, _constGoldProduction, _timeToProduceGold;
        private float _healthTimer,_goldTimer;
        public void Start()
        {
            _health = Game.PlayerPersistentData.BaseHealth;
            _timeToRecover = Game.PlayerPersistentData.TimeToRecover;
            _constHealthRecover = Game.PlayerPersistentData.ConstantHealthRecovered;
            _constGoldProduction = Game.PlayerPersistentData.ConstantGoldProduction;
            _timeToProduceGold = Game.PlayerPersistentData.TimeToProduceGold;
        }
        public void TakeDamage(int damage)
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
            //TODO Game Over Screen, animations and effects
            Destroy(gameObject);
            LevelController.Instance.ShowLoseScreen();
        }

        private void ProduceGold()
        {
            PlayerData.Instance.GiveGold(_constGoldProduction);
        }
        public new void Update()
        {
            base.Update();
            if (_healthTimer < Time.time)
            {
                RecoverHealth(_constHealthRecover);
                _healthTimer = Time.time + _timeToRecover;
            }

            if (_goldTimer < Time.time)
            {
                ProduceGold();
                _goldTimer = Time.time + _timeToProduceGold;
            }
        }
    }

  
}
