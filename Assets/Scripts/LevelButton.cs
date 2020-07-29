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
    //private GameObject tapAnywhere;
    private GameObject levelLockedPanel;
    private GameObject levelLocked;


    void Start()
    {
        levelNum = int.Parse(buttonText.text);
        levelDetails = GameObject.Find("LevelDetails");
        currentLevel = GameObject.Find("Level " + levelNum + " Details");
        levelLockedPanel = GameObject.Find("LevelLockedPanel");
        levelLocked = GameObject.Find("LevelLocked");
        //tapAnywhere = GameObject.Find("TapAnywhereLevelDetails");

        showUnlockedLevels();
    }

    void showUnlockedLevels()
    {
        if (levelNum <= MainMenu.unlockedLevels)
        {           
            buttonText.color = new Color(0.996f, 0.7137f, 0.0352f, 1f);
            buttonImage.color = new Color(0.2196f, 0.4156f, 0.7335f, 1f);
        }
    }

    public void LoadLevel()
    {
        //FORMAT CODE
        if (levelNum <= MainMenu.unlockedLevels)
        {
            ShowPanels();
        }
        else
        {
            ShowLockedInfo();
        }

            
        //SceneManager.LoadScene(levelNum);

        
        //Rename or replace this method because it doesn't load any level
    }

    public void ShowPanels()
    {
        MainMenu.levelNum = int.Parse(buttonText.text);

        levelDetails.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);
        levelDetails.GetComponent<CanvasGroup>().interactable = true;
        levelDetails.GetComponent<CanvasGroup>().blocksRaycasts = true;

        currentLevel.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);
        currentLevel.GetComponent<CanvasGroup>().interactable = true;
        currentLevel.GetComponent<CanvasGroup>().blocksRaycasts = true;

        //tapAnywhere.GetComponent<Button>;
    }

    public void ShowLockedInfo()
    {
        levelDetails.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);
        levelDetails.GetComponent<CanvasGroup>().interactable = true;
        levelDetails.GetComponent<CanvasGroup>().blocksRaycasts = true;

        levelLockedPanel.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f); 
        levelLockedPanel.GetComponent<CanvasGroup>().interactable = true;
        levelLockedPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;

        levelLocked.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);
        levelLocked.GetComponent<CanvasGroup>().interactable = true;
        levelLocked.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }




    void Update()
    {
        
    }
}
