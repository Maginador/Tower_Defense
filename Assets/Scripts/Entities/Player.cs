using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private PlayerData data;


   public void GetPlayerData()
   {
      data = Game.PlayerData;
   }

}

