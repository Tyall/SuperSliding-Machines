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

    public GameObject debugObject;

    public Material materialGrey; //1
    public Material materialLightBlue; //2
    public Material materialRed; //3
    public Material materialPurple; //4
    public Material materialYellow; //5
    public Material materialOrange; //6
    public Material materialGreen; //7
    public Material materialPink; //8
    public Material materialDarkBlue; //9
    public Material materialBlack; //10

    public static Material[] materialsArray;
    Material material;

    public static int[] ownedVehiclesColors = PlayerProfile.ownedVehiclesColors;
    public static int[] ownedVehicles = PlayerProfile.ownedVehicles;

    int vehCount;
    int materialSelector;
    public static string vehicleName;

    Vector3 position;
    
    Quaternion rotation = new Quaternion(-0.214f, 90f, 0f, -90f);
    Quaternion _rotation = new Quaternion(-0.214f, -11f, 0f, 90f);
    Quaternion _rotationShop = new Quaternion(-0.214f, -255f, 0f, 90f);

    bool firstspawn = true;

  
    //TODO:
    //1. Turn off or tweak the blur
    public void Start()
    {
        InstantiateVehicles();
    }

    
    public void UpdateVehicles()
    {
        
        ownedVehicles = PlayerProfile.ownedVehicles;
        ownedVehiclesColors = PlayerProfile.ownedVehiclesColors;
        InstantiateVehicles();
    }

   
    void CreateVehicle(int vehID, int vehPOS, Quaternion rot, int vehPosInArray)
    {
        Debug.Log("Created vehicle with ID " + vehID);
        switch (vehID)
        {

            case 1:
                var newObject = (GameObject)Instantiate(sedan, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
            case 2:
                newObject = (GameObject)Instantiate(sedanSport, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
            case 3:
                newObject = (GameObject)Instantiate(suv, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
            case 4:
                newObject = (GameObject)Instantiate(hatchbackSport, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
            case 5:
                newObject = (GameObject)Instantiate(suvLuxury, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
            case 6:
                newObject = (GameObject)Instantiate(truck, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
            case 7:
                newObject = (GameObject)Instantiate(van, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
            case 8:
                newObject = (GameObject)Instantiate(police, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
            case 9:
                newObject = (GameObject)Instantiate(race, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
            case 10:
                newObject = (GameObject)Instantiate(raceFuture, GetParkingPosition(vehPOS), rot);
                newObject.name = ("ParkingLotVehicle" + vehPOS);
                AssignMaterials(newObject, vehID, vehPosInArray);
                break;
                
        }
        
    }

    void AssignMaterials(GameObject target, int vehID, int vehPosInArray)
    {
        //Some vehicles required special cases because they have extra parts or just different IDs
        materialsArray = target.GetComponentInChildren<MeshRenderer>().materials;

        if (vehID == 2)
        {
            Transform vehicle = target.transform.Find("body");
            Transform spoiler = vehicle.transform.Find("spoiler");
            Material[] spoilerMaterialsArray;
            materialsArray[1] = AssignColor(vehPosInArray, 0);
            spoilerMaterialsArray = spoiler.GetComponent<MeshRenderer>().materials;
            spoilerMaterialsArray[1] = AssignColor(vehPosInArray, 0);
            spoiler.GetComponent<MeshRenderer>().materials = spoilerMaterialsArray;

        }   
        else if (vehID == 9)
        {
            materialsArray[0] = AssignColor(vehPosInArray, 0);
        }
        else
        {
            materialsArray[1] = AssignColor(vehPosInArray, 0);
        }
        
        target.GetComponentInChildren<MeshRenderer>().materials = materialsArray;

    }

    public void ApplyShopColor(int colorID)
    {

        GameObject target = GameObject.Find("ParkingLotVehicle11");
        int vehID = ownedVehicles[0] ;
        //int vehPosInArray = 0; // Applies color from data array
        
        materialsArray = target.GetComponentInChildren<MeshRenderer>().materials;

        if (vehID == 2)
        {
            Transform vehicle = target.transform.Find("body");
            Transform spoiler = vehicle.transform.Find("spoiler");
            Material[] spoilerMaterialsArray;
            materialsArray[1] = AssignColor(colorID, 1);
            spoilerMaterialsArray = spoiler.GetComponent<MeshRenderer>().materials;
            spoilerMaterialsArray[1] = AssignColor(colorID, 1);
            spoiler.GetComponent<MeshRenderer>().materials = spoilerMaterialsArray;

        }
        else if (vehID == 9)
        {
            materialsArray[0] = AssignColor(colorID, 1);
        }
        else
        {
            materialsArray[1] = AssignColor(colorID, 1);
        }

        target.GetComponentInChildren<MeshRenderer>().materials = materialsArray;
    }


    Material AssignColor(int vehID, int mode)
    {
        //mode 0 = color selection from data array
        //mode 1 = color selection from provided data
        if (mode == 0)
        {
            materialSelector = PlayerProfile.ownedVehiclesColors[vehID];
        }
        else
        {
            materialSelector = vehID;
        }
        switch (materialSelector)
        {

            case 1:
                material = materialGrey;
                break;
            case 2:
                material = materialLightBlue;
                break;
            case 3:
                material = materialRed; 
                break;
            case 4:
                material = materialPurple;
                break;
            case 5:
                material = materialYellow;
                break;
            case 6:
                material = materialOrange;
                break;
            case 7:
                material = materialGreen;
                break;
            case 8:
                material = materialPink;
                break;
            case 9:
                material = materialDarkBlue;
                break;
            case 10:
                material = materialBlack;
                break;
            
        }
        return material;
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
        //vehID -= 1;
        Debug.Log("GetVehName vehId " + vehID);
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
        int place = GarageCameraMovement.currentCameraFocus;
        int currentlySelectedVehID = PlayerProfile.ownedVehicles[0];
        int newlySelectedVehID = PlayerProfile.ownedVehicles[place];
        int currentlySelectedVehColor = PlayerProfile.ownedVehiclesColors[0];
        int newlySelectedVehColor = PlayerProfile.ownedVehiclesColors[place];

        if (ownedVehicles[place] != 0)
        {
            if (place > 0 && place <= 10)
            {
                PlayerProfile.ownedVehicles[place] = currentlySelectedVehID;
                PlayerProfile.ownedVehicles[0] = newlySelectedVehID;
                PlayerProfile.ownedVehiclesColors[place] = currentlySelectedVehColor;
                PlayerProfile.ownedVehiclesColors[0] = newlySelectedVehColor;
                //Test a lot times if it works
                InstantiateVehicles();
            }
        }

    }

   

    public void InstantiateVehicles()
    {
        if(!firstspawn) {
            DestroyInstantiatedVehicles();
        }
               
       
        
        CreateVehicle(ownedVehicles[0], 0, _rotation, 0);
        CreateVehicle(ownedVehicles[0], 11, _rotationShop, 0); //Instantiate current vehicle into a shop scene
        for (int i = 1; i < ownedVehicles.Length; i++)
        {
            if (ownedVehicles[i] > 0)
            {
                CreateVehicle(ownedVehicles[i], i , rotation, i);
            }
            
        }

        
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

    public void DEBUGVehArray()
    {
        
            Debug.Log("OwnedVehs |0: " + ownedVehicles[0] + " |1: " + ownedVehicles[1] + " |2: " + ownedVehicles[2] + " |3: " + ownedVehicles[3] + " |4: " + ownedVehicles[4]);
            Debug.Log("OwnedVehsColors |0: " + ownedVehiclesColors[0] + " |1: " + ownedVehiclesColors[1] + " |2: " + ownedVehiclesColors[2] + " |3: " + ownedVehiclesColors[3] + " |4: " + ownedVehiclesColors[4]);
    }
    

}
