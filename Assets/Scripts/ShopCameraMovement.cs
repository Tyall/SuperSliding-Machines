using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCameraMovement : MonoBehaviour
{
    public static int currentCameraFocus = -1;
    public static int vehicleShopCameraFocus = -1;
    public GameObject MainMenu;
    public GameObject shopFunction;
    public GameObject shopBack;
    public GameObject shopTopBack;
    public GameObject shopSelect;
    public GameObject shopSelectButton;
    public GameObject shopTapAnywhere;
    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject ShopColors;
    public GameObject GarageMngr;
    public GameObject ShopMngr;
    TMPro.TextMeshProUGUI shopFunctionText;
    TMPro.TextMeshProUGUI shopSelectText;
    public bool VehicleBrowserMode = false;
    public bool RepaintMode = false;
    

    [HideInInspector]
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        shopFunctionText = shopFunction.GetComponent<TMPro.TextMeshProUGUI>();
        shopSelectText = shopSelectButton.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerShopButtonRight()
    {
        if(VehicleBrowserMode)
        {
            VehicleShopMoveCameraPrevious();
        }
        else if (RepaintMode)
        {
            ShopMngr.GetComponent<ShopManager>().SwitchColorRight();
        }
        else
        {
            MoveCameraPrevious();
        }
    }

    public void TriggerShopButtonLeft()
    {
        if (VehicleBrowserMode)
        {
            VehicleShopMoveCameraNext();
        }
        else if (RepaintMode)
        {
            ShopMngr.GetComponent<ShopManager>().SwitchColorLeft();
        }
        else 
        {
            MoveCameraNext();
        }
    }

    public void TriggerShopBackButton()
    {
        if (VehicleBrowserMode)
        {
            VehicleBrowserMode = false;
            ShopManager.buyvehMode = false;
            RightArrow.SetActive(true);
            currentCameraFocus = 2;
            vehicleShopCameraFocus = -1;
            anim.Play("shop_set_at_cars");
            shopFunctionText.text = "BUY NEW VEHICLE";
            shopSelectText.text = "SELECT";
        }
        else if (RepaintMode)
        {
            RepaintMode = false;
            GarageMngr.GetComponent<GarageManager>().ApplyShopColor(PlayerProfile.ownedVehiclesColors[0]);
            ShopManager.repaintMode = false;
            HideColorPanel();
            currentCameraFocus = 0;
            shopFunctionText.text = "REPAINT VEHICLE";
            shopSelectText.text = "SELECT";
        }
        else
        {
            GoToMainShopScreen();
            HideShopElements();
            
            
        }
    }

    public void TriggerShopSelectButton()
    {

    }


    public void MoveCameraNext()
    {
        anim.SetFloat("Direction", 1);
        switch (currentCameraFocus)
        {
            case -1:
                anim.Play("shop_zoom", 0, 0);
                currentCameraFocus = 0;
                shopFunctionText.text = "REPAINT VEHICLE"; //desired on end or in the middle of anim
                //LockButtons
                //OnAnimEndUnlockButtons
                break;
            case 0:
                anim.Play("shop_case_0", 0, 0); ;
                currentCameraFocus = 1;
                shopFunctionText.text = "GET MORE COINS";
                break;
            case 1:
                anim.Play("shop_case_1", 0, 0);
                currentCameraFocus = 2;
                shopFunctionText.text = "BUY NEW VEHICLE";
                break;
            case 2:
                anim.Play("shop_case_2", 0, 0);
                currentCameraFocus = 0;
                shopFunctionText.text = "REPAINT VEHICLE";
                break;

        }

    }

    public void MoveCameraPrevious()
    {
        anim.SetFloat("Direction", -1);
        switch (currentCameraFocus)
        {
            
            case 0:
                anim.Play("shop_case_2", 0, 1);
                currentCameraFocus = 2;
                shopFunctionText.text = "BUY NEW VEHICLE";
                break;
            case 1:
                anim.Play("shop_case_0", 0, 1);
                currentCameraFocus = 0;
                shopFunctionText.text = "REPAINT VEHICLE";
                break;
            case 2:
                anim.Play("shop_case_1", 0, 1);
                currentCameraFocus = 1;
                shopFunctionText.text = "GET MORE COINS";
                break;

        }
    }

    public void GoToMainShopScreen()
    {
        switch (currentCameraFocus)
        {
            case 0:
                anim.SetFloat("Direction", -1);
                anim.Play("shop_zoom", 0, 1);
                break;
            case 1:
                anim.SetFloat("Direction", 1);
                anim.Play("shop_case_1_back", 0, 0);
                break;
            case 2:
                anim.SetFloat("Direction", 1);
                anim.Play("shop_case_2_back", 0, 0);
                break;
        }
        
    }

    public void SetVehicleShopCameraOnObject()
    {
        if (currentCameraFocus == 0)
        {
            RepaintMode = true;
        }
        else if (currentCameraFocus == 2 && vehicleShopCameraFocus == -1)
        {
            RightArrow.SetActive(false);
            VehicleBrowserMode = true;
            anim.Play("shop_car_zoom");
            vehicleShopCameraFocus = 1;
            SetVehicleName(vehicleShopCameraFocus);

        }
        
    }

    public void VehicleShopMoveCameraNext()
    {
        anim.SetFloat("Direction", 1);
        if (vehicleShopCameraFocus < 10)
        {
            
            
            switch (vehicleShopCameraFocus)
            {

                case 1:
                    RightArrow.SetActive(true);
                    anim.Play("shop_car_1_2", 0, 0);
                    break;
                case 2:
                    anim.Play("shop_car_2_3", 0, 0);
                    break;
                case 3:
                    anim.Play("shop_car_3_4", 0, 0);
                    break;
                case 4:
                    anim.Play("shop_car_4_5", 0, 0);
                    break;
                case 5:
                    anim.Play("shop_car_5_6", 0, 0);
                    break;
                case 6:
                    anim.Play("shop_car_6_7", 0, 0);
                    break;
                case 7:
                    anim.Play("shop_car_7_8", 0, 0);
                    break;
                case 8:
                    anim.Play("shop_car_8_9", 0, 0);
                    break;
                case 9:
                    anim.Play("shop_car_9_10", 0, 0);
                    LeftArrow.SetActive(false);
                    break;

            }
            
            vehicleShopCameraFocus++;
            SetVehicleName(vehicleShopCameraFocus);
        }
        
        
    }

    public void VehicleShopMoveCameraPrevious()
    {

        anim.SetFloat("Direction", -1);
        if (vehicleShopCameraFocus > 1)
        {
            
            
            switch (vehicleShopCameraFocus)
            {
                case 2:
                    RightArrow.SetActive(false);
                    anim.Play("shop_car_1_2", 0, 1);
                    break;
                case 3:
                    anim.Play("shop_car_2_3", 0, 1);
                    break;
                case 4:
                    anim.Play("shop_car_3_4", 0, 1);
                    break;
                case 5:
                    anim.Play("shop_car_4_5", 0, 1);
                    break;
                case 6:
                    anim.Play("shop_car_5_6", 0, 1);
                    break;
                case 7:
                    anim.Play("shop_car_6_7", 0, 1);
                    break;
                case 8:
                    anim.Play("shop_car_7_8", 0, 1);
                    break;
                case 9:
                    anim.Play("shop_car_8_9", 0, 1);
                    break;
                case 10:
                    LeftArrow.SetActive(true);
                    anim.Play("shop_car_9_10", 0, 1);
                    break;

            }
            
            vehicleShopCameraFocus--;
            SetVehicleName(vehicleShopCameraFocus);
        }
    }

    public void SetVehicleName(int vehID)
    {
        shopFunctionText.text = ShopManager.GetVehicleName(vehID);
    }

    public void HideShopElements()
    {
        
        currentCameraFocus = -1;
        MainMenu.GetComponent<MainMenu>().HideShopUI();
        shopTopBack.SetActive(true);
        shopTapAnywhere.SetActive(true);
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

