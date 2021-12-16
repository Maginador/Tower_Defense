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
      BaseHealth = 5;
      TimeToRecover = 120;
      ConstantHealthRecovered = 1;
      ConstantGoldProduction = 1;
      TimeToProduceGold = 10;
      Experience = 0;
      Level = 0;
      SoftCurrency = 0;
      HardCurrency = 0;
   }

   public void SetExperience(int xp)
   {
      Experience += xp;
      CalculateLevel();
   }

   private void CalculateLevel()
   {
      //TODO review level algorithm 
      var xpPerLevel = 100;
      Level = Experience / xpPerLevel;
   }

   public int BaseHealth{ get; set; }
   public int TimeToRecover { get; set; }
   public int ConstantHealthRecovered { get;  set; }
   public int ConstantGoldProduction { get; set; }
   public int TimeToProduceGold { get; set; }
   public int SoftCurrency { get; set; }
   public int HardCurrency { get; set; }
   public int Experience { get; private set; }
   public int Level { get; set; }
}