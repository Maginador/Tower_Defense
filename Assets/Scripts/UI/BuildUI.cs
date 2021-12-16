using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour
{

    [SerializeField] private Text[] towers;
    [SerializeField] private Text[] costs;

    public void BuildTexts(PlayerPersistentData persistentData)
    {
        for(int i =0; i<towers.Length; i++)
        {
            towers[i].text = persistentData.towers[i].textName;
            costs[i].text = persistentData.towers[i].initialCost.ToString();
            
        }
        
    }
}
