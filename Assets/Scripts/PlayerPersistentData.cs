using System.Collections;
using ScriptableObjects;
using UnityEngine;

public class PlayerPersistentData
{
   //Add list of boosts 
   //Add list of stats 
   //Add list of power ups 
   public int _initialGold;
   public TowerData[] towers;

   public PlayerPersistentData(TowerData[] data, int initialGold)
   {
      towers = data;
      _initialGold = initialGold;
      BaseHealth = 100;
      TimeToRecover = 120;
      ConstantHealthRecovered = 1;
      ConstantGoldProduction = 1;
      TimeToProduceGold = 10;
   }

   public int BaseHealth{ get; set; }
   public int TimeToRecover { get; set; }
   public int ConstantHealthRecovered { get; set; }
   public int ConstantGoldProduction { get; set; }
   public int TimeToProduceGold { get; set; }
}