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

    // Start is called before the first frame update
    void Start()
    {
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
            HideShopElements();
        }
    }

    public void TriggerShopSelectButton()
    {

    }


    public void MoveCameraNext()
    {
        if (currentCameraFocus == -1 || currentCameraFocus == 2) // -1- main shop screen
        {
            currentCameraFocus = 0;  
            transform.position = new Vector3(46f, 111.5f, -269.2f);
            transform.rotation = new Quaternion(3.022f, 10f, 1.018f, 70f);
            shopFunctionText.text = "REPAINT VEHICLE";

        }
        else if (currentCameraFocus == 0)  // 0- repaint vehicle screen - zoom on vehicle in the garage
        {
            currentCameraFocus = 1;
            transform.position = new Vector3(41.6f, 111.6f, -264f);
            transform.rotation = new Quaternion(3.022f, 7f, 1.018f, 70f);
            shopFunctionText.text = "GET MORE COINS";
        }
        else if (currentCameraFocus == 1) // 1- premium goods
        {
            currentCameraFocus = 2;
            transform.position = new Vector3(36.6f, 113.3f, -233.4f);
            transform.rotation = new Quaternion(1.022f, 230f, -16.018f, 70f);
            shopFunctionText.text = "BUY NEW VEHICLE";
        }
       
    }

    public void MoveCameraPrevious()
    {
        if (currentCameraFocus == 0)
        {
            currentCameraFocus = 2;
            transform.position = new Vector3(36.6f, 113.3f, -233.4f);
            transform.rotation = new Quaternion(1.022f, 230f, -16.018f, 70f);
            shopFunctionText.text = "BUY NEW VEHICLE";
        }
        else if (currentCameraFocus == 1)
        {
            currentCameraFocus = 0;
            transform.position = new Vector3(46f, 111.5f, -269.2f);
            transform.rotation = new Quaternion(3.022f, 10f, 1.018f, 70f);
            shopFunctionText.text = "REPAINT VEHICLE";
        }
        else if (currentCameraFocus == 2)
        {
            currentCameraFocus = 1;
            transform.position = new Vector3(41.6f, 111.6f, -264f);
            transform.rotation = new Quaternion(3.022f, 7f, 1.018f, 70f);
            shopFunctionText.text = "GET MORE COINS";
        }
    }

    public void SetVehicleShopCameraOnObject()
    {
        if (currentCameraFocus == 2 && vehicleShopCameraFocus == -1)
        {
            VehicleBrowserMode = true;
            transform.position = new Vector3(38.22f, 111.5f, -238f);
            transform.rotation = new Quaternion(1.022f, 290f, -15.018f, 20f);
            vehicleShopCameraFocus = 1;
            SetVehicleName(vehicleShopCameraFocus);

        }
    }

    public void VehicleShopMoveCameraNext()
    {
        if (vehicleShopCameraFocus < 10)
        {
            vehicleShopCameraFocus += 1;
            transform.position = new Vector3(transform.position.x + 1.75f, transform.position.y, transform.position.z - 1);
            SetVehicleName(vehicleShopCameraFocus);
        }
    }

    public void VehicleShopMoveCameraPrevious()
    {
        if (vehicleShopCameraFocus > 1)
        {
            vehicleShopCameraFocus -= 1;
            transform.position = new Vector3(transform.position.x - 1.75f, transform.position.y, transform.position.z + 1);
            SetVehicleName(vehicleShopCameraFocus);
        }
    }

    public void SetVehicleName(int vehID)
    {
        shopFunctionText.text = ShopManager.GetVehicleName(vehID);
    }

    public void HideShopElements()
    {
        transform.position = new Vector3(43.4f, 113.4f, -280.75f);
        transform.rotation = new Quaternion(1.8f, 6.02f, -2.2f, 65f); //z -1.7 kinda worked except garage
        currentCameraFocus = -1;
        MainMenu.HideShopUI();
        shopTopBack.SetActive(true);
        shopTapAnywhere.SetActive(true);
    }
}
