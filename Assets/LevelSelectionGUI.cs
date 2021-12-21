using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionGUI : MonoBehaviour
{
    [SerializeField] private LevelData[] levelDataList;
    [SerializeField] private Text description, levelName;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private Button[] levelsButtons;

    private LevelData _currentData; 
    // Start is called before the first frame update

    public void Start()
    {
        for (int i = 0; i < levelDataList.Length; i++)
        {
            levelDataList[i].id = i;
        }

        for (int i = 1; i < levelsButtons.Length; i++)
        {
            var requirement = PlayerPrefs.GetInt("LevelCompleted" + (i-1),0);
            if (requirement == 0)
            {
                levelsButtons[i].interactable = (false);
            }
            else
            {
                levelsButtons[i].interactable = (true);

            }
        }
    }
    public void OnEnable()
    {
        CheckCurrentLevel();
    }

    private void CheckCurrentLevel()
    {
        //TODO: get currenty level from backend instead of playerprefs
        var curLevel = PlayerPrefs.GetInt("CurrentLevel",0);

    }

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
