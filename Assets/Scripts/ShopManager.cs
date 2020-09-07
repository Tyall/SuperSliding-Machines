using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopSelect;
    public GameObject shopSelectButton;
    public GameObject shopFunction;
    TMPro.TextMeshProUGUI shopFunctionText;
    TMPro.TextMeshProUGUI shopSelectText;
    public TMPro.TextMeshProUGUI shopRightButtonText;
    public GameObject vehicleShopButtons;
    
    
    public GameObject ShopColors;
    int selectedColor;
    public GameObject GarageMngr;
    GameObject[] colorButtons; //check if Button[] works

    int shopMode;

    Vector3 scaled = new Vector3(1.3f, 1.3f, 1);
    Vector3 unscaled = new Vector3(1, 1, 1);

    public static bool repaintMode = false;

    // Start is called before the first frame update
    void Start()
    {
        shopFunctionText = shopFunction.GetComponent<TMPro.TextMeshProUGUI>();
        shopSelectText = shopSelectButton.GetComponent<TMPro.TextMeshProUGUI>();
        colorButtons = GameObject.FindGameObjectsWithTag("ColorButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectFunction()
    {
        shopMode = ShopCameraMovement.currentCameraFocus;
        Debug.Log("select");
        Debug.Log("shop mode: " + shopMode);
        switch (shopMode)
        {
            case 0:
                Debug.Log("Paint");
                RepaintVehicle();
                break;
            case 1:
                //premium goods
                Debug.Log("Premium");
                break;
            case 2:
                //vehicle buy menu
                ShowVehicleShopUI();
                break;
        }
    }

    public void ShowVehicleShopUI()
    {
        shopFunctionText.text = "VEHNAME";
        shopRightButtonText.text = "BUY";
    }

    public void HideVehicleShopUI()
    {
        shopFunctionText.text = "BUY NEW VEHICLE";
        shopRightButtonText.text = "SELECT";
    }

    public void ColorButtonSelected(int buttonId)
    {
        ResetColorButtons();
        colorButtons[buttonId].LeanScale(scaled, 0.1f);
        shopFunctionText.text = GetColorName(buttonId+1);
        GarageMngr.GetComponent<GarageManager>().ApplyShopColor(buttonId + 1);
        selectedColor = buttonId+1;
        Debug.Log("colbutselected");
    }

    public void ResetColorButtons()
    {
        foreach (GameObject btn in colorButtons)
        {
            btn.LeanScale(unscaled, 0.1f);
        }
    }

    public void SwitchColorLeft()
    {
        if (selectedColor > 1)
        {
            ColorButtonSelected(selectedColor-2);
        }
    }

    public void SwitchColorRight()
    {
        if (selectedColor < 10)
        {
            ColorButtonSelected(selectedColor);
        }
    }
    public string GetColorName(int buttonId)
    {
        switch (buttonId)
        {
            case 1:
                return "GREY";
            case 2:
                return "LIGHT BLUE";
            case 3:
                return "RED";
            case 4:
                return "PURPLE";
            case 5:
                return "YELLOW";
            case 6:
                return "ORANGE";
            case 7:
                return "GREEN";
            case 8:
                return "PINK";
            case 9:
                return "DARK BLUE";
            case 10:
                return "BLACK";
        }
        return null;
    }
    public static string GetVehicleName(int vehID)
    {
        switch (vehID) //tweak vehicle prices
        {
            case 1:
                return "SEDAN \n1000 COINS";
            case 2:
                return "SEDAN SPORT \n5000 COINS";
            case 3:
                return "SUV \n10000 COINS";
            case 4:
                return "HATCHBACK SPORT \n15000 COINS";
            case 5:
                return "SUV LUXURY \n20000 COINS";
            case 6:
                return "TRUCK \n25000 COINS";
            case 7:
                return "VAN \n30000 COINS";
            case 8:
                return "POLICE \n35000 COINS";
            case 9:
                return "RACE \n50000 COINS";
            case 10:
                return "RACE FUTURE \n100000 COINS";
        }
        return null;
    }

    public void BuyVehicle()
    {

    }

    public void RepaintVehicle()
    {
        
        if (!repaintMode)
        {
            shopFunctionText.text = "SELECT A COLOR";
            shopSelectText.text = "REPAINT";
            ShowColorPanel();
            repaintMode = true;
        }
        else
        {
            //Repainting done there. Data arrays changed
            PlayerProfile.ownedVehiclesColors[0] = selectedColor;
            GarageManager.ownedVehiclesColors[0] = selectedColor; //not working
       }
    }

    public void PremiumShop()
    {

    }

    public void ShowColorPanel()
    {
        ShopColors.LeanMoveLocalY(230, 0.5f);
    }

    public void HideColorPanel()
    {
        ShopColors.LeanMoveLocalY(630, 0.3f);
    }
}
