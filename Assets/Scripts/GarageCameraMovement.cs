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

    public GameObject _GarageUI;
    public GameObject _GarageButtons;
    public GameObject LeftArrow;
    public GameObject RightArrow;

    [HideInInspector]
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentCameraFocus = -1;
        vehNameText = vehNameGameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        
    }

    public void MoveCameraNext()
    {
        anim.SetFloat("Direction", 1);
        if (currentCameraFocus < 10)
        {
            switch (currentCameraFocus)
            {
                case -1:
                    LeftArrow.SetActive(false);
                    anim.Play("garage_zoom", 0, 0);
                    vehNameText.text = "SELECTED VEHICLE";
                    break;
                case 0:
                    LeftArrow.SetActive(true); ;
                    anim.Play("garage_car_0_1", 0, 0);
                    break;
                case 1:
                    HideCurrentVehicle();
                    anim.Play("garage_car_1_2", 0, 0);
                    break;
                case 2:
                    anim.Play("garage_car_2_3", 0, 0);
                    break;
                case 3:
                    anim.Play("garage_car_3_4", 0, 0);
                    break;
                case 4:
                    anim.Play("garage_car_4_5", 0, 0);
                    break;
                case 5:
                    anim.Play("garage_car_5_6", 0, 0);
                    break;
                case 6:
                    anim.Play("garage_car_6_7", 0, 0);
                    break;
                case 7:
                    anim.Play("garage_car_7_8", 0, 0);
                    break;
                case 8:
                    anim.Play("garage_car_8_9", 0, 0);
                    break;
                case 9:
                    RightArrow.SetActive(false);
                    anim.Play("garage_car_9_10", 0, 0);
                    break;

            }
            currentCameraFocus++;
            SetVehicleName(currentCameraFocus);
        }
        
    }

    public void MoveCameraPrevious()
    {
        anim.SetFloat("Direction", -1);
        if (currentCameraFocus > -1)
        {
            switch (currentCameraFocus)
            {
                case 0:
                    HideGarageUI();
                    anim.Play("garage_zoom", 0, 1);
                    garageGoBack.SetActive(true);
                    garageTapAnywhere.SetActive(true);
                    break;
                case 1:
                    ShowCurrentVehicle();
                    LeftArrow.SetActive(false); ;
                    anim.Play("garage_car_0_1", 0, 1);
                    break;
                case 2:
                    anim.Play("garage_car_1_2", 0, 1);
                    break;
                case 3:
                    anim.Play("garage_car_2_3", 0, 1);
                    break;
                case 4:
                    anim.Play("garage_car_3_4", 0, 1);
                    break;
                case 5:
                    anim.Play("garage_car_4_5", 0, 1);
                    break;
                case 6:
                    anim.Play("garage_car_5_6", 0, 1);
                    break;
                case 7:
                    anim.Play("garage_car_6_7", 0, 1);
                    break;
                case 8:
                    anim.Play("garage_car_7_8", 0, 1);
                    break;
                case 9:
                    anim.Play("garage_car_8_9", 0, 1);
                    break;
                case 10:
                    RightArrow.SetActive(true);
                    anim.Play("garage_car_9_10", 0, 1);
                    break;

            }
            currentCameraFocus--;
            SetVehicleName(currentCameraFocus);
        }
    }    

    void SetVehicleName(int currentCameraFocus)
    {
       vehNameText.text = GarageManager.GetVehicleName(GarageManager.ownedVehicles[currentCameraFocus]);
    }

    public void HideGarageSelectorElements()
    {
        ShowCurrentVehicle();
        HideGarageUI();
        garageGoBack.SetActive(true);
        garageTapAnywhere.SetActive(true);
        currentCameraFocus = -1;
        anim.Play("garage_set_at_cars");
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
        anim.Play("garage_set_at_selected_car");
        currentCameraFocus = 0;
        vehNameText.text = "SELECTED VEHICLE";
        ShowCurrentVehicle();
    }
    public void DebugShowCurrentCameraSpotID()
    {
        Debug.Log(currentCameraFocus);
    }

    public void HideGarageUI()
    {
        _GarageUI.LeanMoveLocalY(650, 0.7f);
        _GarageButtons.LeanMoveLocalY(1080, 0.7f);
    }
}
