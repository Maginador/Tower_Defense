using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private PlayerPersistentData _persistentData;


   public void GetPlayerData()
   {
      _persistentData = Game.PlayerPersistentData;
   }

}

