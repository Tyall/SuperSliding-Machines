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

    private GameObject levelDetails;
    private GameObject currentLevel;
    private GameObject tapAnywhere;


    void Start()
    {
        levelNum = int.Parse(buttonText.text);
        levelDetails = GameObject.Find("LevelDetails");
        currentLevel = GameObject.Find("Level " + levelNum + " Details");
        tapAnywhere = GameObject.Find("TapAnywhereLevelDetails");
    }

    public void LoadLevel()
    {
        //FORMAT CODE
        Debug.Log(levelNum);
        //SceneManager.LoadScene(levelNum);

        ShowPanels();
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

   

    void Update()
    {
        
    }
}
