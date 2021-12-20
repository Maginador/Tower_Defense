using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {


        public static TimeManager Instance;
        private List<UnityEvent> _timeEventListener;
        private List<float> _eventTimeLimit;
        private List<float> _eventTimer;
        private List<string> _eventId;
        private Dictionary<string, int> _eventsDictionary;
        private int _currentIndex;
        
        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
            _timeEventListener = new List<UnityEvent>();
            _eventTimeLimit = new List<float>();
            _eventTimer = new List<float>();
            _eventId = new List<String>();
            _eventsDictionary = new Dictionary<string, int>();
        }

        public void Update()
        {
            for (int i = 0; i < _timeEventListener.Count; i++)
            {
                if (_eventTimer[i] < Time.time)
                {
                    _eventTimer[i] = Time.time + _eventTimeLimit[i];
                    _timeEventListener[i].Invoke();
                }
            }
        }
        public void CreateNewTimeEvent(string id, float time, UnityAction listener)
        {
            _timeEventListener.Add(new UnityEvent());
            _timeEventListener[_currentIndex].AddListener(listener);
            _eventTimeLimit.Add(time);
            _eventTimer.Add(0);
            _eventId.Add(id);
            _eventsDictionary.Add(id,_currentIndex);
            _currentIndex++;
        }

        public void AddListenerToEvent(string id, UnityAction listener)
        {
            if(_eventsDictionary.ContainsKey(id))
                _timeEventListener[_eventsDictionary[id]].AddListener(listener);
        }
        public void SetDefaultTime()
        {
            Time.timeScale = 1;

        }

        public void Set2XTime()
        {
            Time.timeScale = 2;

        }

        public void Set3XTime()
        {
            Time.timeScale = 3;

        }

        public void StopGame()
        {
            Time.timeScale = 0;
        }
    }
}
