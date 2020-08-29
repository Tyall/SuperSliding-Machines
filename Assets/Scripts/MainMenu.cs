using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField][HideInInspector] private PostProcessVolume postProcessVolume;
        
    private GameObject[] levelPanels;
    public static int levelNum;

    static GameObject placeholderCoins;
    static GameObject placeholderExp;

    public GameObject Logo;
    public GameObject Camera;
    public GameObject GarageUI;
    public GameObject GarageButtons;
    public GameObject ShopUI;
    public GameObject ShopButtons;
    public GameObject Coins;
    public GameObject Exp;
    public GameObject LevelSelectionBG;

    static GameObject _GarageUI;
    static GameObject _GarageButtons;
    static GameObject _ShopUI;
    static GameObject _ShopButtons;
  

    public void Start()
    {
        //LevelPanels is an array of all panels involving details of the levels. It has to be handled with FindWithTag
        levelPanels = GameObject.FindGameObjectsWithTag("LevelDetails");

        Camera cam = Camera.GetComponent<Camera>();
        postProcessVolume = cam.GetComponent<PostProcessVolume>();      

        _GarageUI = GarageUI;
        _GarageButtons = GarageButtons;
        _ShopUI = ShopUI;
        _ShopButtons = ShopButtons;

        UpdateUI();
    }

    public void PlayGame()
    {
        HideLogo();
        ShowLevelSelection();
    }

   public static void UpdateUI()
    {
        //This method is used only once, at start of the Main Menu script so it's not really taking much resources with the Finds
        placeholderCoins = GameObject.Find("CoinsText");
        placeholderExp = GameObject.Find("ExpText");
        placeholderCoins.GetComponent<TMPro.TextMeshProUGUI>().text = "" + PlayerProfile.playerCoins;
        placeholderExp.GetComponent<TMPro.TextMeshProUGUI>().text = "" + PlayerProfile.playerExp;
    }

    public void HideLogo()
    {
        Logo.LeanMoveLocalY(750, 0.5f);
        HideCoinsAndExp();
    }

    public void ShowLogo()
    {
        Logo.LeanMoveLocalY(345, 0.5f);
        ShowCoinsAndExp();
    }

    public void ShowLevelSelection()
    {
        LevelSelectionBG.LeanMoveLocalY(100, 0.7f); 
        ChangeDepthOfField(0.1f);
    }

    public void ShowGarageUI()
    {
        GarageUI.LeanMoveLocalY(420, 0.7f);
        GarageButtons.LeanMoveLocalY(80, 0.7f);
    }

    public void HideGarageUI()
    {
        _GarageUI.LeanMoveLocalY(650, 0.7f); 
        _GarageButtons.LeanMoveLocalY(1080, 0.7f);
    }

    public void ShowShopUI()
    {
        ShopUI.LeanMoveLocalY(420, 0.7f);
        ShopButtons.LeanMoveLocalY(80, 0.7f);
    }

    public void HideShopUI()
    {
        _ShopUI.LeanMoveLocalY(650, 0.7f);
        _ShopButtons.LeanMoveLocalY(1080, 0.7f);
    }

    public void ShowCoinsAndExp()
    {
        Coins.LeanMoveLocalY(472, 0.7f); 
        Exp.LeanMoveLocalY(387, 0.7f);
    }
    
    public void HideCoinsAndExp()
    {
        Coins.LeanMoveLocalY(672, 0.7f); 
        Exp.LeanMoveLocalY(587, 0.7f);
    }

    public void ChangeDepthOfField(float value)
    {
        var volume = this.postProcessVolume;
        var profile = volume.sharedProfile;
        var depthOfField = profile.GetSetting<DepthOfField>();
        depthOfField.aperture.value = value;
        //TODO: Try to make it progressive
    }

    public void HideLevelSelection()
    {
        LevelSelectionBG.LeanMoveLocalY(1000, 0.7f);
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
