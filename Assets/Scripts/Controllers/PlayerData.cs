using System;
using Entities;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData Instance;
        private PlayerPersistentData _data;
        private int _currentGold;
        private UnityEvent _onGoldChanged;
        private UnityEvent _onXpChanged;
        private UnityEvent _onHcChanged;
        public Base baseEntity;
        private int _receivedGold;

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }
            Instance = this;
            
            _onGoldChanged = new UnityEvent();
            _onXpChanged = new UnityEvent();
            _onHcChanged = new UnityEvent();
        }

        public void Start()
        {
            SetData(Game.PlayerPersistentData);
        }

        public void AddOnHCChangedListener(UnityAction listener)
        {
            _onHcChanged.AddListener(listener);
        }
        public void AddOnGoldChangedListener(UnityAction listener)
        {
            _onGoldChanged.AddListener(listener);
        }
        public void RemoveOnGoldChangedListener(UnityAction listener)
        {
            _onGoldChanged.RemoveListener(listener);
        }public void AddOnXPChangedListener(UnityAction listener)
        {
            _onXpChanged.AddListener(listener);
        }
        public void RemoveOnXPChangedListener(UnityAction listener)
        {
            _onXpChanged.RemoveListener(listener);
        }
        

        public void SetData(PlayerPersistentData pData)
        {
            _data = pData;
            _currentGold = _data.InitialGold;
            _receivedGold = 0;
        }

        public void GiveGold(int gold)
        {
            _currentGold += gold;
            _receivedGold += gold;
            _onGoldChanged.Invoke();
        }

        public void SpendGold(int gold)
        {
            _currentGold -= gold;
            _onGoldChanged.Invoke();

        }
        
        public bool HasEnoughGold(int gold)
        {
            return _currentGold >= gold;
        }

        public int CurrentGoldAmount()
        {
            return _currentGold;
        }
        
        public int GoldReceivedThisLevel()
        {
            return _receivedGold;
        }
        public void AddExperience(int xp)
        {
            _data.SetExperience(xp);
            _onXpChanged.Invoke();
        }

        public int CurrentXp()
        {
            return _data.Experience;
        }
        
        public int CurrentLvl()
        {
            return _data.Level;
        }

        public bool HasEnoughHc(int hc)
        {
            return _data.HardCurrency >= hc;;
        }

        public void SpendHc(int cost)
        {
            _data.HardCurrency -= cost;
            _onHcChanged.Invoke();

        }

        public int CurrentHcAmount()
        {
            return _data.HardCurrency;
        }
    }
}