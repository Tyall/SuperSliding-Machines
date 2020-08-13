using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageCameraMovement : MonoBehaviour
{

    public static int currentCameraFocus = -1;
    public GameObject vehNameGameObject;
    public GameObject garageGoBack;
    public GameObject garageTapAnywhere;
    TMPro.TextMeshProUGUI vehNameText;
    GameObject currentVehicleGameObject;

    //TODO: IF A SPOT YOU'RE ABOUT TO LOOK AT IS EMPTY - DON'T SHOW IT
    //probably CheckIfEmpty() will do
    //CheckIfEmpty { if ownedVehicles[currentCamLookingAt-1] - 0, currentCamLookingAt+=1, MoveCameraNext/Prev }
    void Start()
    {
        currentCameraFocus = -1;
        vehNameText = vehNameGameObject.GetComponent<TMPro.TextMeshProUGUI>();
        vehNameText.text = "CURRENT VEHICLE";//Rework with currently selected, maybe add another UI element 
        //or perhaps currently selected vehicle stands in front of them all and camera has also a special place like -1 is menu, 0 is selected car, others are parking lots
        //show top menu only when clicked anywhere
        currentVehicleGameObject = GameObject.Find("ParkingLotVehicle0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCameraNext()
    {
        Debug.Log("Camera place before " + currentCameraFocus);
        if (currentCameraFocus == -1)
        {
            currentCameraFocus = 0;
            transform.position = new Vector3(47.7f, 111.5f, -288.9f);
            transform.rotation = new Quaternion(3.022f, 116.409f, 1.018f, 70f) ;
            vehNameText.text = "SELECTED VEHICLE";
        }
        else if (currentCameraFocus == 0)
        {
            currentCameraFocus = 1;
            transform.position = new Vector3(53.6f, 111.3f, -285.54f);
            transform.rotation = new Quaternion(3.022f, 116.409f, 1.018f, 60f);
            SetVehicleName(currentCameraFocus);
            HideCurrentVehicle();
        }
        else if (currentCameraFocus < 5) 
        {

            currentCameraFocus++;
            transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y , transform.position.z - 2.2f);
            SetVehicleName(currentCameraFocus);

        }
        else if (currentCameraFocus == 5)
        {
            currentCameraFocus = 6;
            transform.position = new Vector3(66.5f, 110.9f, -295.3f);
            SetVehicleName(currentCameraFocus);
        }
        else if (currentCameraFocus < 10)
        {
            currentCameraFocus++;
            transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z + 2.2f);
            SetVehicleName(currentCameraFocus);
        }
        else if(currentCameraFocus == 10)
        {
            currentCameraFocus = -1;
            transform.position = new Vector3(43.38f, 113.37f, -280.75f);
            transform.rotation = new Quaternion(6.168f, 129.36f, -0.316f, 65f);
            MainMenu.HideGarageUI();
            garageGoBack.SetActive(true);
            garageTapAnywhere.SetActive(true);
            vehNameText.text = "UI SAYS GOODBYE";
        }
        Debug.Log("Camera place after " +currentCameraFocus);
        
    }

    public void MoveCameraPrevious()
    {
        //Show CurrentVehicle GameObject in case 1 so it will be visible in states -1 and 0
        Debug.Log("Camera place before " + currentCameraFocus);
        if (currentCameraFocus == 0)
        {
            //Back to garage main screen
            transform.position = new Vector3(43.38f, 113.37f, -280.75f);
            transform.rotation = new Quaternion(6.168f, 129.36f, -0.316f, 65f);
            MainMenu.HideGarageUI();
            garageGoBack.SetActive(true);
            garageTapAnywhere.SetActive(true);
            currentCameraFocus = -1;
            vehNameText.text = "UI SAYS GOODBYE";
        }
        else if (currentCameraFocus == 1)
        {
            transform.position = new Vector3(47.7f, 111.5f, -288.9f);
            transform.rotation = new Quaternion(3.022f, 116.409f, 1.018f, 70f);
            currentCameraFocus = 0;
            vehNameText.text = "SELECTED VEHICLE";
            ShowCurrentVehicle();
        }
        else if (currentCameraFocus < 6)
        {
            currentCameraFocus--;
            transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z + 2.2f);
            SetVehicleName(currentCameraFocus);
        }
        else if (currentCameraFocus == 6)
        {
            currentCameraFocus = 5;
            transform.position = new Vector3(52.4f, 111.3f, -294.3f);
            SetVehicleName(currentCameraFocus);
        }
        else if (currentCameraFocus <= 10)
        {
            currentCameraFocus--;
            transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z - 2.2f);
            SetVehicleName(currentCameraFocus);
        }
        Debug.Log("Camera place after " + currentCameraFocus);

    }

    void SetVehicleName(int currentCameraFocus)
    {
       vehNameText.text = GarageManager.GetVehicleName(GarageManager.ownedVehicles[currentCameraFocus]);
    }

    public void HideGarageSelectorElements()
    {
        currentCameraFocus = 0;
        MoveCameraPrevious();
    }

    public void ShowCurrentVehicle()
    {
        GameObject.Find("ParkingLotVehicle0").transform.position = new Vector3(54.34f, 110.22f, -292.46f);
    }

    public void HideCurrentVehicle()
    {
        GameObject.Find("ParkingLotVehicle0").transform.position = new Vector3(40f, 100f, -250f);
    }

    public void FocusCameraOnSelectedVehicle()
    {
        transform.position = new Vector3(47.7f, 111.5f, -288.9f);
        transform.rotation = new Quaternion(3.022f, 116.409f, 1.018f, 70f);
        currentCameraFocus = 0;
        vehNameText.text = "SELECTED VEHICLE";
        ShowCurrentVehicle();
    }
    public void DebugShowCurrentCameraSpotID()
    {
        Debug.Log(currentCameraFocus);
    }
}
