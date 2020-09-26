using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    
    public static TextMeshProUGUI buttonText;
    private static GameObject levelDetails;
    private static GameObject currentLevel;
    private static GameObject levelLockedPanel;
    private static GameObject levelLocked;
    private static GameObject levelStartButton;
    void Start()
    {

    }

    public static void LoadLevel(int levelNum)
    {

        levelDetails = GameObject.Find("LevelDetails");
        currentLevel = GameObject.Find("Level " + levelNum + " Details");
        levelLockedPanel = GameObject.Find("LevelLockedPanel");
        levelLocked = GameObject.Find("LevelLocked");
        levelStartButton = GameObject.Find("StartLevelButton");
        if (levelNum <= PlayerProfile.unlockedLevels)
        {
            ShowPanels();
        }
        else
        {
            ShowLockedInfo();
        }
        MainMenu.levelNum = levelNum;
    }

    public static void ShowPanels()
    {
        EnableComponent(levelStartButton);
        EnableComponent(currentLevel);
        EnableComponent(levelDetails);
    }

    public static void ShowLockedInfo()
    {
        EnableComponent(levelDetails);
        EnableComponent(levelLockedPanel);
        EnableComponent(levelLocked);
    }

    static void EnableComponent(GameObject obj)
    {
        obj.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);
        obj.GetComponent<CanvasGroup>().interactable = true;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
