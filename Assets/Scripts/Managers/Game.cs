using System;
using ScriptableObjects;
using UnityEngine;

public class Game : MonoBehaviour
{
    //mocked data 
    [SerializeField] private TowerData[] towers;
    [SerializeField] private int initialGold;
    public void Awake()
    {
        if (PlayerData == null)
        {
            PlayerData = GetPlayerData();
        }
    }

    private PlayerData GetPlayerData()
    {
        //TODO: check if there is stored data to recover or create a new user 
        return new PlayerData(towers,initialGold);
    }

    public static PlayerData PlayerData { get; set; }
    
    
}