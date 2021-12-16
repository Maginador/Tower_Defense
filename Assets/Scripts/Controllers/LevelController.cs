using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

namespace Controllers
{
    public class LevelController : MonoBehaviour
    {
        public GameObject[] floor;
        public Color[] colorTable;
        public Texture2D levelMap;
        public GameObject startPoint;
        public int startPointIndex;
        public GameObject waypoint;
        public List<Transform> waypoints;
        public static LevelController instance;
    
        //Path Builder Variables
        private int[,] _levelMatrix;
        private int _width, _height;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            PrepareLevel();
        }

        public void PrepareLevel()
        {
            _width = levelMap.width;
            _height = levelMap.height;
            _levelMatrix = new int[_width, _height];

            for (int i = 0; i < _width; i++)
            {
                for (int o = 0; o < _height; o++)
                {
                    var tile = SelectTile(levelMap.GetPixel(i, o));
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
            }
        }

        public void BuildPath()
        {
            //TODO Change algorithm and data structure to support multiple paths 
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
    }

    public class Tiles
    {
        public const int SpawnSpot = 1;
        public const int TowerSpot = 2;
        public const int Path = 3;
        public const int EndSpot = 4;
    }
}
