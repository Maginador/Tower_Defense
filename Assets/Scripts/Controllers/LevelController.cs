using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class LevelController : MonoBehaviour
    {
        public GameObject[] floor;
        public Color[] colorTable;
        public Texture2D levelMap;
        public GameObject startPoint;
        public List<Transform> waypoints;
        public static LevelController instance;

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
            var width = levelMap.width;
            var height = levelMap.height;
            for (int i = 0; i < width; i++)
            {
                for (int o = 0; o < height; o++)
                {
                    var tile = SelectTile(levelMap.GetPixel(i, o));
                    BuildTile(i, o, tile);
                }
            }
            
        }

        public int SelectTile(Color color)
        {
            for (int i = 0; i < colorTable.Length; i++)
            {
                Debug.Log(color + " : " + colorTable[i] + " : " + (color == colorTable[i]));

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
            if (tile == Tiles.SpawnSpot)
            {
                startPoint = obj;
            }
        }

        public void BuildWaypoints()
        {
        
        }
    
    
    }

    public class Tiles
    {
        public const int SpawnSpot = 1;
    }
}
