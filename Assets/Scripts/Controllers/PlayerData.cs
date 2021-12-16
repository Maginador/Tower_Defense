using System;
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
        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }
            Instance = this;
            
            _onGoldChanged = new UnityEvent();
        }

        public void Start()
        {
            SetData(Game.PlayerPersistentData);
        }

        public void AddOnGoldChangedListener(UnityAction listener)
        {
            _onGoldChanged.AddListener(listener);
        }
        public void RemoveOnGoldChangedListener(UnityAction listener)
        {
            _onGoldChanged.RemoveListener(listener);
        }
        

        public void SetData(PlayerPersistentData pData)
        {
            _data = pData;
            _currentGold = _data._initialGold;
        }

        public void GiveGold(int gold)
        {
            _currentGold += gold;
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
    }
}