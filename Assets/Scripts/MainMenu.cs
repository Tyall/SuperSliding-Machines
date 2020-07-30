using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private PostProcessVolume postProcessVolume;
        
    private GameObject[] levelPanels;
    public static int levelNum;

    static GameObject placeholderCoins;
    static GameObject placeholderExp;

    //This should be in player profile class
    static int coins = 0;
    static int experience = 0;
    public static int unlockedLevels = 2;

    public void Start()
    {
       levelPanels = GameObject.FindGameObjectsWithTag("LevelDetails");

       placeholderCoins = GameObject.Find("CoinsText");
       placeholderExp = GameObject.Find("ExpText");

       placeholderCoins.GetComponent<TMPro.TextMeshProUGUI>().text = "" + coins;
       placeholderExp.GetComponent<TMPro.TextMeshProUGUI>().text = "" + experience;
    }

    public void PlayGame()
    {
        HideLogo();
        ShowLevelSelection();
    }

   

    public void HideLogo()
    {
        GameObject.Find("Logo").LeanMoveLocalY(750, 0.5f);
    }

    public void ShowLogo()
    {
        GameObject.Find("Logo").LeanMoveLocalY(345, 0.5f);
    }

    public void ShowLevelSelection()
    {
        GameObject.Find("LevelSelectionBG").LeanMoveLocalY(100, 0.7f);
        Camera cam = GameObject.Find("Camera").GetComponent<Camera>();
        postProcessVolume = cam.GetComponent<PostProcessVolume>();
        ChangeDepthOfField(0.1f);
        
    }

    public void ChangeDepthOfField(float value)
    {
        var volume = this.postProcessVolume;
        var profile = volume.sharedProfile;
        var depthOfField = profile.GetSetting<DepthOfField>();
        depthOfField.aperture.value = value;
        //make it progressive?
    }

    public void HideLevelSelection()
    {
        
        GameObject.Find("LevelSelectionBG").LeanMoveLocalY(1000, 0.7f);
        Camera cam = GameObject.Find("Camera").GetComponent<Camera>();
        postProcessVolume = cam.GetComponent<PostProcessVolume>();
        ChangeDepthOfField(4.1f);

        ShowLogo();
    }

    public void HidePanels()
    {
        Debug.Log("HidingPanels");
        
        foreach(GameObject panel in levelPanels)
        {
            panel.GetComponent<CanvasGroup>().LeanAlpha(0, 0.2f);
            panel.GetComponent<CanvasGroup>().interactable = false;
            panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

    }

    public void LoadLevel()
    {
        Debug.Log("Load Level");
        SceneManager.LoadScene(levelNum);
        
        
    }

    public void ChooseLevelPopUp()
    {
        Debug.Log("Choose a level before clicking play!");
    }

    //PLACEHOLDER - MOVE IT TO A PLAYER PROFILE CLASS 
    //Testing if the code works
    public static void UpdateProfile(int coin, int exp)
    {
        experience += exp;
        coins += coin;
        
    }

    
}
