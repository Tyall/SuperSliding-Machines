using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    

    public void PlayGame()
    {
        AnimateLogo();
        ShowLevelSelection();
    }

    public void SelectLevel()
    {
        //case level 1:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //case level 2:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void AnimateLogo()
    {
        GameObject.Find("Logo").LeanMoveLocalY(750, 0.5f);
    }

    public void ShowLevelSelection()
    {      
        GameObject.Find("LevelSelectionBG").LeanMoveLocalY(100, 1f);

        //PLACEHOLDER - To be replaced with blur shader instead of white image
        Image ui = GameObject.Find("MenuBlur").GetComponent<Image>();
        ui.CrossFadeAlpha(125f, 1, false);
       
    }
    
}
