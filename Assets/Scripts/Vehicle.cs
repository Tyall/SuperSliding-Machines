using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Vehicle 
{
   // public GameObject sedan;
   // public GameObject sedanSport;
   // public GameObject suv;
   // public GameObject hatchbackSport;
   // public GameObject suvLuxury;
   // public GameObject truck;
   // public GameObject van;
   // public GameObject police;
   // public GameObject race;
   // public GameObject raceFuture;


    int vehicleID;
    string color;
    int garageParkingID;
    GameObject vehMesh;

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

    
    public Vehicle(GameObject vehMesh, int vehicleID, string color, int garageParkingID)
    {
        Debug.Log("Vehicle constructor");
        this.vehicleID = vehicleID;
        this.vehMesh = vehMesh;
        CreateAsset(vehMesh);
        



    }

    void CreateAsset(GameObject vehMesh)
    {
        
    }

}
