using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageCameraMovement : MonoBehaviour
{

    public int currentCameraFocusPlace = -1;
    public GameObject vehNameGameObject;
    public GameObject garageGoBack;
    public GameObject garageTapAnywhere;
    TMPro.TextMeshProUGUI vehNameText;

    // Start is called before the first frame update
    void Start()
    {
        currentCameraFocusPlace = -1;
        vehNameText = vehNameGameObject.GetComponent<TMPro.TextMeshProUGUI>();
        vehNameText.text = "CURRENT VEHICLE";//Rework with currently selected, maybe add another UI element 
        //or perhaps currently selected vehicle stands in front of them all and camera has also a special place like -1 is menu, 0 is selected car, others are parking lots
        //show top menu only when clicked anywhere
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCameraNext()
    {
        //Hide CurrentVehicle GameObject in case 1 so it won't be visible through 2-10 cases and don't forget to show it if it overlaps 10->1
        if (currentCameraFocusPlace == -1)
        {
            currentCameraFocusPlace = 0;
            transform.position = new Vector3(47.7f, 111.5f, -288.9f);
            transform.rotation = new Quaternion(3.022f, 116.409f, 1.018f, 70f) ;
            vehNameText.text = "VEHICLE " + currentCameraFocusPlace; //temporarily
        }
        else if (currentCameraFocusPlace == 0)
        {
            currentCameraFocusPlace = 1;
            transform.position = new Vector3(53.6f, 111.3f, -285.54f);
            transform.rotation = new Quaternion(3.022f, 116.409f, 1.018f, 60f);
            vehNameText.text = "VEHICLE  " + currentCameraFocusPlace;
        }
        else if (currentCameraFocusPlace < 5) //if <1;5> then alg X, if 5 then switch to 2nd lane if <6;10> alg, 10->1 ect
        {

            currentCameraFocusPlace++;
            transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y , transform.position.z - 2.2f);
            vehNameText.text = "VEHICLE  " + currentCameraFocusPlace;

        }
        else if (currentCameraFocusPlace == 5)
        {
            currentCameraFocusPlace = 6;
            transform.position = new Vector3(66.5f, 110.9f, -295.3f);
            vehNameText.text = "VEHICLE  " + currentCameraFocusPlace;
        }
        else if (currentCameraFocusPlace < 10)
        {
            currentCameraFocusPlace++;
            transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z + 2.2f);
            vehNameText.text = "VEHICLE  " + currentCameraFocusPlace;
        }
        else if(currentCameraFocusPlace == 10)
        {
            currentCameraFocusPlace = -1;
            transform.position = new Vector3(43.38f, 113.37f, -280.75f);
            transform.rotation = new Quaternion(6.168f, 129.36f, -0.316f, 65f);
            MainMenu.HideGarageUI();
            garageGoBack.SetActive(true);
            garageTapAnywhere.SetActive(true);
            vehNameText.text = "UI SAYS GOODBYE";
        }
        
    }

    public void MoveCameraPrevious()
    {
        //Show CurrentVehicle GameObject in case 1 so it will be visible in states -1 and 0
        
        if (currentCameraFocusPlace == 0)
        {
            //Back to garage main screen
            transform.position = new Vector3(43.38f, 113.37f, -280.75f);
            transform.rotation = new Quaternion(6.168f, 129.36f, -0.316f, 65f);
            MainMenu.HideGarageUI();
            garageGoBack.SetActive(true);
            garageTapAnywhere.SetActive(true);
            currentCameraFocusPlace = -1;
            vehNameText.text = "UI SAYS GOODBYE";
        }
        else if (currentCameraFocusPlace == 1)
        {
            transform.position = new Vector3(47.7f, 111.5f, -288.9f);
            transform.rotation = new Quaternion(3.022f, 116.409f, 1.018f, 70f);
            currentCameraFocusPlace = 0;
            vehNameText.text = "VEHICLE " + currentCameraFocusPlace; //temporarily
        }
        else if (currentCameraFocusPlace < 6)
        {
            currentCameraFocusPlace--;
            transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z + 2.2f);
            vehNameText.text = "VEHICLE " + currentCameraFocusPlace;
        }
        else if (currentCameraFocusPlace == 6)
        {
            currentCameraFocusPlace = 5;
            transform.position = new Vector3(52.4f, 111.3f, -294.3f);
            vehNameText.text = "VEHICLE  " + currentCameraFocusPlace;
        }
        else if (currentCameraFocusPlace < 10)
        {
            currentCameraFocusPlace--;
            transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z - 2.2f);
            vehNameText.text = "VEHICLE  " + currentCameraFocusPlace;
        }

    }
}
