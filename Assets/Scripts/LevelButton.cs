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



    void Start()
    {
        levelNum = int.Parse(buttonText.text);
        levelDetails = GameObject.Find("LevelDetails");
    }

    public void LoadLevel()
    {
        Debug.Log(levelNum);
        //SceneManager.LoadScene(levelNum);

        levelDetails.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);// = 1;
        levelDetails.GetComponent<CanvasGroup>().interactable = true;
        levelDetails.GetComponent<CanvasGroup>().blocksRaycasts = true;

        //Make a loop for every level button to handle its desired level desc etc.



        //TODO
        //Instead of loading level right after clicking the button, open a pop-up explaining
        //logic of chosen level and showing minimap (?)
        //Level will be selected when PLAY is clicked
    }
    
    void Update()
    {
        
    }
}
