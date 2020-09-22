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

    private GameObject levelDetails;
    private GameObject currentLevel;
    private GameObject levelLockedPanel;
    private GameObject levelLocked;
    private GameObject levelStartButton;
    private int unlockedLevels;


    void Start()
    {
        unlockedLevels = PlayerProfile.unlockedLevels;
        levelNum = int.Parse(buttonText.text);
        levelDetails = GameObject.Find("LevelDetails");
        currentLevel = GameObject.Find("Level " + levelNum + " Details");
        levelLockedPanel = GameObject.Find("LevelLockedPanel");
        levelLocked = GameObject.Find("LevelLocked");
        levelStartButton = GameObject.Find("StartLevelButton");

        ShowUnlockedLevels();
    }

    private void Awake()
    {
        unlockedLevels = PlayerProfile.unlockedLevels;
           
    }
    public void ShowUnlockedLevels()
    {
        if (levelNum <= unlockedLevels)
        {           
            buttonText.color = new Color(0.996f, 0.7137f, 0.0352f, 1f);
            buttonImage.color = new Color(0.2196f, 0.4156f, 0.7335f, 1f);
        }
    }


    public void LoadLevel()
    {
        if (levelNum <= unlockedLevels)
        {
            ShowPanels();
        }
        else
        {
            ShowLockedInfo();
        }
    }

    public void ShowPanels()
    {
        MainMenu.levelNum = int.Parse(buttonText.text);

        EnableComponent(levelStartButton);
        EnableComponent(currentLevel);
        EnableComponent(levelDetails);

    }

    public void ShowLockedInfo()
    {
        EnableComponent(levelDetails);
        EnableComponent(levelLockedPanel);
        EnableComponent(levelLocked);
    }

    void EnableComponent(GameObject obj)
    {
        obj.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);
        obj.GetComponent<CanvasGroup>().interactable = true;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
