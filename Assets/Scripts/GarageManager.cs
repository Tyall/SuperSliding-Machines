using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageManager : MonoBehaviour
{
    public GameObject sedan;
    public GameObject sedanSport;
    public GameObject suv;
    public GameObject hatchbackSport;
    public GameObject suvLuxury;
    public GameObject truck;
    public GameObject van;
    public GameObject police;
    public GameObject race;
    public GameObject raceFuture;
    public static int[] ownedVehicles = PlayerProfile.ownedVehicles;
    int vehCount;
    Vector3 position;
    static string vehicleName;
    public int selectedVehicle = PlayerProfile.ownedVehicles[0];
    Quaternion rotation = new Quaternion(-0.214f, 90f, 0f, -90f);
    Quaternion _rotation = new Quaternion(-0.214f, -11f, 0f, 90f);
    Quaternion _rotationShop = new Quaternion(-0.214f, -255f, 0f, 90f);

    bool firstspawn = true;

  
    //TODO:
    //1. Clean the code
    //2. Turn off or tweak the blur
    void Start()
    {
        InstantiateVehicles();
    }

    void GetVehicleCount() //Is it even useful?
    {
        for (int j = 0; j < ownedVehicles.Length; j++)
        {
            if (ownedVehicles[j] > 0)
                vehCount++;
        }
        Debug.Log("vehCount = " + vehCount);
    }
    void CreateVehicle(int vehID, int vehPOS, Quaternion rot)
    {


        Debug.Log("Created vehicle with ID " + vehID);
        switch (vehID)
        {

            case 1:
                var newObject = (GameObject)Instantiate(sedan, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;
            case 2:
                newObject = (GameObject)Instantiate(sedanSport, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;
            case 3:
                newObject = (GameObject)Instantiate(suv, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;
            case 4:
                newObject = (GameObject)Instantiate(hatchbackSport, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;
            case 5:
                newObject = (GameObject)Instantiate(suvLuxury, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;
            case 6:
                newObject = (GameObject)Instantiate(truck, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;
            case 7:
                newObject = (GameObject)Instantiate(van, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;
            case 8:
                newObject = (GameObject)Instantiate(police, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;
            case 9:
                newObject = (GameObject)Instantiate(race, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;
            case 10:
                newObject = (GameObject)Instantiate(raceFuture, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                break;

        }

    }

    void AssignColor()
    {
        //This will be problematic
        //Colors will have to go together with reinstantiating and other things so switching a vehicle would also switch its color etc.
    }

    Vector3 GetParkingPosition(int GaragePosition)
    {

        Debug.Log("Garage position: " + GaragePosition);
        switch (GaragePosition)
        {
            case 0:
                position = new Vector3(54.34f, 110.22f, -292.46f);//Current Vehicle
                break;
            case 1:
                position = new Vector3(58.35f, 110.21f, -288.78f);
                break;
            case 2:
                position = new Vector3(58.35f, 110.22f, -291.25f);
                break;
            case 3:
                position = new Vector3(58.35f, 110.23f, -293.7f);
                break;
            case 4:
                position = new Vector3(58.35f, 110.24f, -296.21f);
                break;
            case 5:
                position = new Vector3(58.35f, 110.19f, -298.62f);
                break;
            case 6:
                position = new Vector3(71.35f, 110.18f, -298.62f);
                break;
            case 7:
                position = new Vector3(71.35f, 110.18f, -296.21f);
                break;
            case 8:
                position = new Vector3(71.35f, 110.17f, -293.7f);
                break;
            case 9:
                position = new Vector3(71.35f, 110.17f, -291.25f);
                break;
            case 10:
                position = new Vector3(71.35f, 110.16f, -288.78f);
                break;
            case 11: //Shop
                position = new Vector3(47.9f, 110.23f, -263.66f);
                break;
        }
        return position;
    }

    public static string GetVehicleName(int vehID)
    {
        switch (vehID)
        {
            case 0:
                vehicleName = "EMPTY PLACE";
                break;
            case 1:
                vehicleName = "SEDAN";
                break;
            case 2:
                vehicleName = "SEDAN SPORT";
                break;
            case 3:
                vehicleName = "SUV";
                break;
            case 4:
                vehicleName = "HATCHBACK SPORT";
                break;
            case 5:
                vehicleName = "LUXURY SUV";
                break;
            case 6:
                vehicleName = "TRUCK";
                break;
            case 7:
                vehicleName = "VAN";
                break;
            case 8:
                vehicleName = "POLICE";
                break;
            case 9:
                vehicleName = "RACE";
                break;
            case 10:
                vehicleName = "RACE FUTURE";
                break;
        }
        return vehicleName;
    }

    public void SetCurrentVehicle()
    {
        int place = GarageCameraMovement.currentCameraFocusPlace;
        int currentlySelectedVehID = PlayerProfile.ownedVehicles[0];
        int newlySelectedVehID = PlayerProfile.ownedVehicles[place];

        if (ownedVehicles[place] != 0)
        {
            if (place > 0 && place <= 10)
            {
                PlayerProfile.ownedVehicles[place] = currentlySelectedVehID;
                PlayerProfile.ownedVehicles[0] = newlySelectedVehID;

                InstantiateVehicles();
            }
        }

    }

   

    public void InstantiateVehicles()
    {
        if(!firstspawn) {
            DestroyInstantiatedVehicles();
        }
        
        //remove vehs instantiated before
        
        GetVehicleCount();
        
        CreateVehicle(ownedVehicles[0], 0, _rotation);
        CreateVehicle(ownedVehicles[0], 11, _rotationShop);//Instantiate current vehicle into a shop scene
        for (int i = 1; i < ownedVehicles.Length; i++)
        {
            if (ownedVehicles[i] > 0)
            {
                CreateVehicle(ownedVehicles[i], i , rotation);
            }
            
        }

        AssignColor();
        firstspawn = false;
    }

    public void DestroyInstantiatedVehicles()
    {
        for (int id = 0; id<=11; id++)
        {
            GameObject toDestroy = GameObject.Find("ParkingLotVehicle" + id);
            Destroy(toDestroy);
            Debug.Log("ParkingLotVehicle" + id + " destroyed");
        }
        
    }
    

}
