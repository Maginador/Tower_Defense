using System;
using ScriptableObjects;
using UnityEngine;

public class Game : MonoBehaviour
{
    //mocked persistentData 
    [SerializeField] private TowerData[] towers;
    [SerializeField] private int initialGold;
    public void Awake()
    {
        if (PlayerPersistentData == null)
        {
            PlayerPersistentData = GetPlayerData();
        }
    }

    private PlayerPersistentData GetPlayerData()
    {
        //TODO: check if there is stored persistentData to recover or create a new user 
        return new PlayerPersistentData(towers,initialGold);
    }

    public static PlayerPersistentData PlayerPersistentData { get; set; }

    public static void RunLevel(LevelData currentData)
    {
        throw new NotImplementedException();
    }
}