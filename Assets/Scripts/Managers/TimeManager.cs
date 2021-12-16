using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {


        public static TimeManager Instance;
    
        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
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
