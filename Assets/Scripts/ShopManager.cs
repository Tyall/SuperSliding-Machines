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
    public GameObject ShopVehicleBuy;
    public GameObject PopupEnoughMoney;
    public GameObject PopupUniversal;
    public TMPro.TextMeshProUGUI PopupText;



    public GameObject ShopColors;
    int selectedColor;
    public GameObject GarageMngr;
    GameObject[] colorButtons; //check if Button[] works

    public GameObject btnGrey;
    public GameObject btnLBlue;
    public GameObject btnRed;
    public GameObject btnPurple;
    public GameObject btnYellow;
    public GameObject btnOrange;
    public GameObject btnGreen;
    public GameObject btnPink;
    public GameObject btnDBlue;
    public GameObject btnBlack;

    int shopMode;

    Vector3 scaled = new Vector3(1.3f, 1.3f, 1);
    Vector3 unscaled = new Vector3(1, 1, 1);

    public static bool repaintMode = false;
    public static bool buyvehMode = false;
    void Start()
    {
        shopFunctionText = shopFunction.GetComponent<TMPro.TextMeshProUGUI>();
        shopSelectText = shopSelectButton.GetComponent<TMPro.TextMeshProUGUI>();
        colorButtons = GameObject.FindGameObjectsWithTag("ColorButton");
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
                BuyVehicle();
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
        GetCorrectColorButton(buttonId).LeanScale(scaled, 0.1f); //scales wrong button in build app
        shopFunctionText.text = GetColorName(buttonId+1);
        GarageMngr.GetComponent<GarageManager>().ApplyShopColor(buttonId + 1);
        selectedColor = buttonId+1;
        Debug.Log("colbutselected");
    }

    public GameObject GetCorrectColorButton(int btnID)
    {
        switch (btnID+1)
        {
            case 1:
                return btnGrey;
            case 2:
                return btnLBlue;
            case 3:
                return btnRed;
            case 4:
                return btnPurple;
            case 5:
                return btnYellow;
            case 6:
                return btnOrange;
            case 7:
                return btnGreen;
            case 8:
                return btnPink;
            case 9:
                return btnDBlue;
            case 10:
                return btnBlack;

        }
        return null;
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
        else if (selectedColor == 1)
        {
            ColorButtonSelected(9);
        }
    }

    public void SwitchColorRight()
    {
        if (selectedColor < 10)
        {
            ColorButtonSelected(selectedColor);
        }
        else if (selectedColor == 10)
        {
            ColorButtonSelected(0);
        }
    }

    public void FinalizeVehiclePurchase()
    {
        PlayerProfile.playerCoins -= GetVehiclePrice(ShopCameraMovement.vehicleShopCameraFocus);
        int place = GetFirstEmptyParkingPlace();
        PlayerProfile.ownedVehicles[place] = ShopCameraMovement.vehicleShopCameraFocus;
        PlayerProfile.ownedVehiclesColors[place] = GetVehicleBasicColor(ShopCameraMovement.vehicleShopCameraFocus);

        PopupEnoughMoney.SetActive(false);
        PopupUniversal.SetActive(true);
        PopupText.text = "VEHICLE HAS BEEN SUCCESFULLY BOUGHT AND PLACED IN YOUR GARAGE";
        //DO THERE
        //Vehicle has been succesfully bought popup!
        //Maybe reuse the other popup window, change text and "Go Back" will suit somehow
        //Or even change "Go Back" to "Okay"!
    }

    public void NotEnoughFounds()
    {
        PopupText.text = "YOU DON'T HAVE ENOUGH COINS TO BUY THIS VEHICLE";
        ShopVehicleBuy.SetActive(true);
        PopupUniversal.SetActive(true);
    }

    public void NotEnoughtParkingSpace()
    {
        PopupText.text = "YOUR GARAGE IS FULL. SELL ONE OF YOUR VEHICLES BEFORE BUYING A NEW ONE";
        ShopVehicleBuy.SetActive(true);
        PopupUniversal.SetActive(true);
    }

    public void VehBuyGoBack()
    {
        ShopVehicleBuy.SetActive(false);
        PopupEnoughMoney.SetActive(false);
        PopupUniversal.SetActive(false);
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

   

    public bool CheckIfEmptyParkingSpot()
    {
        int number = 0;
        for (int i=0; i<11; i++)
        {
            if (PlayerProfile.ownedVehicles[i] == 0)
            {
                number++;
            }
        }
        if (number != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetFirstEmptyParkingPlace()
    {
        int place=-1;

        for (int i = 0; i < 11; i++)
        {
            if (PlayerProfile.ownedVehicles[i] == 0)
            {
                place = i;
                return place; //Does it even work?
            }
        }
        return place;
        
    }

    public int GetVehicleBasicColor(int vehID)
    {
        switch (vehID) 
        {
            case 1:
                return 3;
            case 2:
                return 3;
            case 3:
                return 7;
            case 4:
                return 7;
            case 5:
                return 5;
            case 6:
                return 7;
            case 7:
                return 2;
            case 8:
                return 1;
            case 9:
                return 3;
            case 10:
                return 2;
        }
        return -1;
    }
    public int GetVehiclePrice(int vehID)
    {
        switch (vehID) //tweak vehicle prices
        {
            case 1:
                return 1000;
            case 2:
                return 5000;
            case 3:
                return 10000;
            case 4:
                return 15000;
            case 5:
                return 20000;
            case 6:
                return 25000;
            case 7:
                return 30000;
            case 8:
                return 35000;
            case 9:
                return 50000;
            case 10:
                return 100000;
        }
        return 9999999;
    }

    public void BuyVehicle()
    {
        Debug.Log("BuyVeh");
        if (!buyvehMode)
        {
            ShowVehicleShopUI();
            buyvehMode = true;
        }
        else
        {
            Debug.Log("BuyVehStage2");
            if (PlayerProfile.playerCoins>= GetVehiclePrice(ShopCameraMovement.vehicleShopCameraFocus))
            {
                if (CheckIfEmptyParkingSpot())
                {
                    ShopVehicleBuy.SetActive(true);
                    PopupEnoughMoney.SetActive(true);
                }
                else
                {
                    NotEnoughtParkingSpace();
                }
            }
            else
            {
                NotEnoughFounds();
            }
           
        }
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
            GarageManager.ownedVehiclesColors[0] = selectedColor; 
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
