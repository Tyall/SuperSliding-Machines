using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Vehicle 
{
    int vehicleID;
    string color;
    int garageParkingID;
    GameObject vehModel = GameObject.Find("vehicle1");
    string prefabPath = "Assets/Visuals/Vehicles";


    public Vehicle(int vehicleID, string color, int garageParkingID)
    {
        Debug.Log("Vehicle constructor");
        this.vehicleID = vehicleID;
        AssignModel(vehicleID);
        



    }

    void AssignModel(int vehID)
    {
        Debug.Log("Assigning model " + vehID);
        switch (vehID)
        {
            case 1:
                //Object prefab = EditorUtility.CreateEmptyPrefab("Assets/Prefabs/Vehicles/sedan.prefab");
                
                Debug.Log("Model assigned");
                break;
            //case 2:
                 
                





        }
    }
    



    //VEHICLE ID LIST
    //1. sedan
    //2. sedanSport
    //3. suv
    //4. hatchbackSport
    //5. suvLuxury
    //6. truck
    //7. van
    //8. police
    //9. race
    //10. raceFuture

}
