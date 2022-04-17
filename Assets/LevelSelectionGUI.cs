using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionGUI : MonoBehaviour
{
    [SerializeField] private LevelData[] levelDataList;
    [SerializeField] private Text description, levelName;
    [SerializeField] private GameObject[] stars;

    private LevelData _currentData; 
    // Start is called before the first frame update
    
    public void Play()
    {
        Game.RunLevel(_currentData);
    }
    public void ShowInformation(int levelId)
    {
        
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
        var data = levelDataList[levelId-1];
        description.text = data.description;
        levelName.text = data.levelName;
        _currentData = data;
        //TODO: get quantity of stars from backend instead of playerprefs
        var starsQ = PlayerPrefs.GetInt("LevelStars_" + levelId,0);
        for (int i = 0; i < starsQ; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
