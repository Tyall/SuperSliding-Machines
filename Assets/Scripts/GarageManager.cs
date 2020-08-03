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
    static int[] ownedVehicles = PlayerProfile.ownedVehicles;
    int vehCount;
    Vector3 position;
    Quaternion rotation;
    void Start()
    {
        GetVehicleCount();
        for( int i=0; i < ownedVehicles.Length; i++)
        {
            if (ownedVehicles[i] > 0)
            {
                CreateVehicle(ownedVehicles[i], i);
            }
            
        }
        
        AssignColor();
        
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
    void CreateVehicle(int vehID, int vehPOS)
    {
        Debug.Log("Created vehicle with ID " + vehID);
        switch (vehID)
        {
            case 1:
                Instantiate(sedan, GetParkingPosition(vehPOS), GetParkingRotation(vehPOS));
                break;
            case 2:
                Instantiate(sedanSport, GetParkingPosition(vehPOS), transform.rotation);
                break;
            case 3:
                Instantiate(suv, GetParkingPosition(vehPOS), transform.rotation);
                break;
            case 4:
                Instantiate(hatchbackSport, GetParkingPosition(vehPOS), transform.rotation);
                break;
            case 5:
                Instantiate(suvLuxury, GetParkingPosition(vehPOS), transform.rotation);
                break;
            case 6:
                Instantiate(truck, GetParkingPosition(vehPOS), transform.rotation);
                break;
            case 7:
                Instantiate(van, GetParkingPosition(vehPOS), transform.rotation);
                break;
            case 8:
                Instantiate(police, GetParkingPosition(vehPOS), transform.rotation);
                break;
            case 9:
                Instantiate(race, GetParkingPosition(vehPOS), transform.rotation);
                break;
            case 10:
                Instantiate(raceFuture, GetParkingPosition(vehPOS), transform.rotation);
                break;

        }
        
    }

    void AssignColor()
    {
        //This will be problematic
    }

    Vector3 GetParkingPosition(int GaragePosition)
    {
        switch (GaragePosition)
        {
            case 1:
                position = new Vector3(1, 0, 0); //get values from inspector, move objects and save the exact values and rotations                
                break;
            case 2:
                position = new Vector3(2, 0, 0);                 
                break;
            case 3:
                position = new Vector3(3, 0, 0);                 
                break;
            case 4:
                position = new Vector3(4, 1, 0);               
                break;
            case 5:
                position = new Vector3(5, 2, 0);
                break;
            case 6:
                position = new Vector3(6, 0, 1);                 
                break;
            case 7:
                position = new Vector3(7, 0, 2);                 
                break;
            case 8:
                position = new Vector3(8, 0, 0);                 
                break;
            case 9:
                position = new Vector3(9, 4, 0);                 
                break;
            case 10:
                position = new Vector3(10, 0, 5);                 
                break;
        }
        return position;
    }

    Quaternion GetParkingRotation(int GaragePosition)
    {
        switch (GaragePosition)
        {
            case 1:
                rotation = new Quaternion(1, 0, 0, 0); //get values from inspector, move objects and save the exact values and rotations                 
                break;
            case 2:
                rotation = new Quaternion(1, 0, 0, 0);                 
                break;
            case 3:
                rotation = new Quaternion(1, 0, 0, 0);                 
                break;
            case 4:
                rotation = new Quaternion(1, 0, 0, 0);                 
                break;
            case 5:                 
                rotation = new Quaternion(1, 0, 0, 0);   
                break;
            case 6:
                rotation = new Quaternion(1, 0, 0, 0);                 
                break;
            case 7:
                rotation = new Quaternion(1, 0, 0, 0);                 
                break;
            case 8:
                rotation = new Quaternion(1, 0, 0, 0);                 
                break;
            case 9:
                rotation = new Quaternion(1, 0, 0, 0);                 
                break;
            case 10:
                rotation = new Quaternion(1, 0, 0, 0);                 
                break;
        }
        return rotation;
    }
    
}
