using System.Collections;
using ScriptableObjects;
using UnityEngine;

public class PlayerData
{
   //Add list of boosts 
   //Add list of stats 
   //Add list of power ups 
   public int _initialGold;
   public TowerData[] towers;

   public PlayerData(TowerData[] data, int initialGold)
   {
      towers = data;
      _initialGold = initialGold;
   }
}