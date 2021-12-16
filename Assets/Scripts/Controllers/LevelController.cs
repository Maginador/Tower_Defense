using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class LevelController : MonoBehaviour
    {

        public GameObject startPoint;
        public List<Transform> waypoints;
        public static LevelController instance;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        public void BuildPath()
        {
        
        }

        public void BuildWaypoints()
        {
        
        }
    
    
    }
}
