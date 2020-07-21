using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{

    [HideInInspector] public int levelNum;
    public TextMeshProUGUI buttonText;

    
    void Start()
    {
        levelNum = int.Parse(buttonText.text);
    }

    public void LoadLevel()
    {
        Debug.Log(levelNum);
        // SceneManager.LoadScene(levelNum);
        //GameObject.Find("Level Details").SetActive(true);
        //GameObject.Find("Level 1 Details").SetActive(true);
        

        //TODO
        //Instead of loading level right after clicking the button, open a pop-up explaining
        //logic of chosen level and showing minimap (?)
        //Level will be selected when PLAY is clicked
    }
    
    void Update()
    {
        
    }
}
