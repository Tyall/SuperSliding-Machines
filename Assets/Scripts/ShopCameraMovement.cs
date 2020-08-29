using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCameraMovement : MonoBehaviour
{
    public static int currentCameraFocus = -1;
    public static int vehicleShopCameraFocus = -1;
    public GameObject shopFunction;
    public GameObject shopBack;
    public GameObject shopTopBack;
    public GameObject shopSelect;
    public GameObject shopTapAnywhere;
    TMPro.TextMeshProUGUI shopFunctionText;
    public bool VehicleBrowserMode = false;

    [HideInInspector]
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        shopFunctionText = shopFunction.GetComponent<TMPro.TextMeshProUGUI>();
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
            currentCameraFocus = 1;
            MoveCameraNext();
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
        //if (currentCameraFocus == -1 || currentCameraFocus == 2) // -1- main shop screen
        //{
        //    //probably gotta separate -1 and 2
        //    currentCameraFocus = 0;  
        //    transform.position = new Vector3(46f, 111.5f, -269.2f);
        //    transform.rotation = new Quaternion(3.022f, 10f, 1.018f, 70f);
        //    shopFunctionText.text = "REPAINT VEHICLE";

        //}
        //else if (currentCameraFocus == 0)  // 0- repaint vehicle screen - zoom on vehicle in the garage
        //{
        //    currentCameraFocus = 1;
        //shopFunctionText.text = "GET MORE COINS";
        //    transform.position = new Vector3(41.6f, 111.6f, -264f);
        //    transform.rotation = new Quaternion(3.022f, 7f, 1.018f, 70f);
        //    
        //}
        //else if (currentCameraFocus == 1) // 1- premium goods
        //{
        //    currentCameraFocus = 2;
        //shopFunctionText.text = "BUY NEW VEHICLE";
        //    transform.position = new Vector3(36.6f, 113.3f, -233.4f);
        //    transform.rotation = new Quaternion(1.022f, 230f, -16.018f, 70f);
        //    
        //}

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
        if (currentCameraFocus == 2 && vehicleShopCameraFocus == -1)
        {
            VehicleBrowserMode = true;
            anim.Play("shop_car_zoom");//check if events have to be done after the animation or is it ok now
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
        //MainMenu.HideShopUI();
        shopTopBack.SetActive(true);
        shopTapAnywhere.SetActive(true);
    }
}
