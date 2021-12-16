using System;
using System.Collections.Generic;
using Entities;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

namespace Controllers
{
    public class LevelController : MonoBehaviour
    {
        public LevelData data;
        public GameObject[] floor;
        public Color[] colorTable;
        public GameObject startPoint;
        public int startPointIndex;
        public GameObject waypoint;
        public List<Transform> waypoints;
        public static LevelController Instance;
        private UnityEvent _onWaveChanged;
        private UnityEvent _onEnemiesQuantityChanged;

        [SerializeField] private InGameUIController uiController;
        //Path Builder Variables
        private int[,] _levelMatrix;
        private int _width, _height;


        private int _wave;
        private int _enemies;
        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }
            Instance = this;
            _onWaveChanged = new UnityEvent();
            _onEnemiesQuantityChanged = new UnityEvent();
            PrepareLevel();
        }

        public void PrepareLevel()
        {
            
            _width = data.levelMap.width;
            _height = data.levelMap.height;
            _levelMatrix = new int[_width, _height];

            for (int i = 0; i < _width; i++)
            {
                for (int o = 0; o < _height; o++)
                {
                    var tile = SelectTile(data.levelMap.GetPixel(i, o));
                    _levelMatrix[i, o] = tile;
                    if (tile == Tiles.SpawnSpot)
                    {
                        startPointIndex = i + (o * _height);
                    }

                    BuildTile(i, o, tile);
                }
            }

            BuildPath();
        }

        public int SelectTile(Color color)
        {
            for (int i = 0; i < colorTable.Length; i++)
            {
                if (color == colorTable[i])
                {
                    return i;
                }
            }

            return 0;
        }
        public void BuildTile(int x, int y, int tile)
        {
            var obj = Instantiate(floor[tile], new Vector3(x,0,y), Quaternion.identity);
            obj.name = x + " , " + y;
            if (tile == Tiles.SpawnSpot)
            {
                startPoint = obj;
            }else if (tile == Tiles.EndSpot)
            {
                PlayerData.Instance.baseEntity = obj.GetComponent<Base>();
            }
        }

        public void BuildPath()
        {
            //TODO Change algorithm and persistentData structure to support multiple paths 
            //TODO create a helper call to fix broken level textures (textures that do not have complete paths for example) 
            var y = Mathf.FloorToInt(startPointIndex / (float)_height);
            var x = startPointIndex % _height;
            var a = _levelMatrix[x, y+1];
            var b = _levelMatrix[x, y-1];
            var c = _levelMatrix[x+1, y];
            var d = _levelMatrix[x-1, y];

            var next = a == Tiles.Path ? x+(y+1)*_height : b == Tiles.Path ? x  +(y-1)*_height : c == Tiles.Path ? x  +1+(y*_height) : d == Tiles.Path ? x -1 +(y*_height) : -1;
            CheckNeighbours(next);
        
        }

        //10 x 10 
        //5 x 6 = 46
        //45/10 = 4.6
        //45%10 = 6
        private void CheckNeighbours(int i)
        {
            var y = Mathf.FloorToInt(i / (float)_width);
            var x = i % _width;
            _levelMatrix[x, y] = -1;
            SpawnWaypoint(x,y);

            var a = _levelMatrix[x, y+1];
            var b = _levelMatrix[x, y-1];
            var c = _levelMatrix[x+1, y];
            var d = _levelMatrix[x-1, y];
            if (a == Tiles.Path)
            {
                CheckNeighbours(x  +(y+1)*_height);
            }
            if (b == Tiles.Path)
            {
                CheckNeighbours(x  +(y-1)*_height);
            }
            if (c == Tiles.Path)
            {
                CheckNeighbours(x+1  +(y*_height));
            }
            if (d == Tiles.Path)
            {
                CheckNeighbours(x-1  +(y*_height));
            }
        }

        private void SpawnWaypoint(int x, int y)
        {
           var point =  Instantiate(waypoint, new Vector3(x, 1, y), Quaternion.identity);
           waypoints.Add(point.transform);
        }

        public int CurrentWave()
        {
            return _wave;
        }

        public int CurrentQuantityOfEnemies()
        {
            return _enemies;
        }

        public void AddOnWaveChangedListener(UnityAction action)
        {
            _onWaveChanged.AddListener(action);
        }
        
        public void AddOnEnemiesQuantityChangedListener(UnityAction action)
        {
            _onEnemiesQuantityChanged.AddListener(action);
        }

        public void ShowWinScreen()
        {
            uiController.ShowWinScreen();
        }
        
        public void ShowLoseScreen()
        {
            uiController.ShowLoseScreen();
        }

        public void SetWave(int currentWave)
        {
            _wave = currentWave;
            _onWaveChanged.Invoke();
        }

        public void SetSpawnedEnemies(int enemiesCount)
        {
            _enemies = enemiesCount;
            _onEnemiesQuantityChanged.Invoke();
        }

        public int GetMaxWaves()
        {
            return data.waves;
        }
    }

    public class Tiles
    {
        public const int SpawnSpot = 1;
        public const int TowerSpot = 2;
        public const int Path = 3;
        public const int EndSpot = 4;
    }
}
