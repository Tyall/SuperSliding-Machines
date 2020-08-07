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
  

    public void Start()
    {
        levelPanels = GameObject.FindGameObjectsWithTag("LevelDetails");

        placeholderCoins = GameObject.Find("CoinsText");
        placeholderExp = GameObject.Find("ExpText");

        UpdateUI();
    }

    public void PlayGame()
    {
        HideLogo();
        ShowLevelSelection();
    }

   public static void UpdateUI()
    {
        placeholderCoins.GetComponent<TMPro.TextMeshProUGUI>().text = "" + PlayerProfile.playerCoins;
        placeholderExp.GetComponent<TMPro.TextMeshProUGUI>().text = "" + PlayerProfile.playerExp;
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
        GameObject.Find("LevelSelectionBG").LeanMoveLocalY(100, 0.7f); //MOVE FIND TO START()
        Camera cam = GameObject.Find("Camera").GetComponent<Camera>();
        postProcessVolume = cam.GetComponent<PostProcessVolume>();
        ChangeDepthOfField(0.1f);
        
    }

    public void ShowGarageUI()
    {
        GameObject.Find("GarageUI").LeanMoveLocalY(420, 0.7f);//MOVE FIND TO START()
        GameObject.Find("GarageButtons").LeanMoveLocalY(80, 0.7f);
        
    }

    static public void HideGarageUI()
    {
        GameObject.Find("GarageUI").LeanMoveLocalY(650, 0.7f);
        GameObject.Find("GarageButtons").LeanMoveLocalY(1080, 0.7f);
        
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
    
}
