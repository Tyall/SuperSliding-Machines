using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopSelect;
    public GameObject shopFunction;
    TMPro.TextMeshProUGUI shopFunctionText;
    public TMPro.TextMeshProUGUI shopRightButtonText;
    public GameObject vehicleShopButtons;
    int shopMode;

    // Start is called before the first frame update
    void Start()
    {
        shopFunctionText = shopFunction.GetComponent<TMPro.TextMeshProUGUI>();
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
                //paint
                Debug.Log("Paint");
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
}
