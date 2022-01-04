using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{

    [HideInInspector] public int levelNum;
    public TextMeshProUGUI buttonText;
    public Image buttonImage;
    public GameObject levelLoader;

    private GameObject levelDetails;
    private GameObject currentLevel;
    private GameObject levelLockedPanel;
    private GameObject levelLocked;
    private GameObject levelStartButton;
    public static volatile int unlockedLevels;

    public int thisLevel;

    void Start()
    {
        unlockedLevels = PlayerProfile.unlockedLevels;
        Debug.Log("Unlocked "+unlockedLevels+" levels");
        levelNum = int.Parse(buttonText.text);
        levelDetails = GameObject.Find("LevelDetails");
        levelLockedPanel = GameObject.Find("LevelLockedPanel");
        levelLocked = GameObject.Find("LevelLocked");
        levelStartButton = GameObject.Find("StartLevelButton");
    }

    private void Awake()
    {
        unlockedLevels = PlayerProfile.unlockedLevels;
    }
    public void ShowUnlockedLevels(int levelNumber)
    {
        if (levelNumber <= unlockedLevels)
        {   
            //temporary solution
            if (levelNumber == 1 ||  levelNumber == 2 ||  levelNumber == 3 || levelNumber == 4 || levelNumber == 5 || levelNumber ==7 || levelNumber == 9 || levelNumber == 13)
            {
                buttonText.color = new Color(0.996f, 0.7137f, 0.0352f, 1f);
                buttonImage.color = new Color(0.2196f, 0.4156f, 0.7335f, 1f);
            }
            else 
            {
                // buttonText.color = new Color(f, 0f, 0f, 1f);
                // buttonImage.color = new Color(0.4f, 0f, 1f, 1f);
                //buttonImage.color = new Color(0.2196f, 0.4156f, 0.7335f, 1f);
                //buttonText.color = new Color(0.2196f, 0.4156f, 0.7335f, 1f);
                SetAlternativeColor();
            }
            
        }
    }

    public void SetLevelNumber(int levelNumber)
    {
        thisLevel = levelNumber;
    }

    public void LoadLevel()
    {
       LevelLoader.LoadLevel(thisLevel);
    }

    public void SetAlternativeColor()
    {
        // buttonText.color = new Color(1f, 0f, 0f, 0.25f);
        // buttonImage.color = new Color(0.4f, 0f, 1f, 0.25f);
        buttonImage.color = new Color(0.2196f, 0.4156f, 0.7335f, 0.25f);
        buttonText.color = new Color(0.2196f, 0.4156f, 0.7335f, 0.25f);
    }
}
