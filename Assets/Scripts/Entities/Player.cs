using Managers;
using UnityEngine;

namespace Entities
{
   public class Player : MonoBehaviour
   {
      private PlayerPersistentData _persistentData;


      public void GetPlayerData()
      {
         _persistentData = Game.PlayerPersistentData;
      }

   }
}

