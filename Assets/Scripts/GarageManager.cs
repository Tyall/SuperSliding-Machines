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
    
    
    void Start()
    {
        GetVehicleCount();
        for( int i=0; i < ownedVehicles.Length; i++)
        {
            if (ownedVehicles[i] > 0)
            {
                CreateVehicle(ownedVehicles[i], i+1);
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
        Quaternion rotation = new Quaternion(-0.214f, 90f, 0f, -90f);
        Debug.Log("Created vehicle with ID " + vehID);
        switch (vehID)
        {
            case 1:
                Instantiate(sedan, GetParkingPosition(vehPOS), rotation);
                break;
            case 2:
                Instantiate(sedanSport, GetParkingPosition(vehPOS), rotation);
                break;
            case 3:
                Instantiate(suv, GetParkingPosition(vehPOS), rotation);
                break;
            case 4:
                Instantiate(hatchbackSport, GetParkingPosition(vehPOS), rotation);
                break;
            case 5:
                Instantiate(suvLuxury, GetParkingPosition(vehPOS), rotation);
                break;
            case 6:
                Instantiate(truck, GetParkingPosition(vehPOS), rotation);
                break;
            case 7:
                Instantiate(van, GetParkingPosition(vehPOS), rotation);
                break;
            case 8:
                Instantiate(police, GetParkingPosition(vehPOS), rotation);
                break;
            case 9:
                Instantiate(race, GetParkingPosition(vehPOS), rotation);
                break;
            case 10:
                Instantiate(raceFuture, GetParkingPosition(vehPOS), rotation);
                break;

        }
        
    }

    void AssignColor()
    {
        //This will be problematic
    }

    Vector3 GetParkingPosition(int GaragePosition)
    {
        
        Debug.Log("Garage position: " + GaragePosition);
        switch (GaragePosition)
        {
            case 1:
                position = new Vector3(58.35f, 110.21f, -288.78f); //get values from inspector, move objects and save the exact values and rotations                
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
                position = new Vector3(58.35f, 110.25f, -298.62f);
                break;
            case 6:
                position = new Vector3(71.35f, 110.29f, -296.21f);
                break;
            case 7:
                position = new Vector3(71.35f, 110.28f, -298.62f);
                break;
            case 8:
                position = new Vector3(71.35f, 110.27f, -293.7f);
                break;
            case 9:
                position = new Vector3(71.35f, 110.26f, -291.25f);
                break;
            case 10:
                position = new Vector3(71.35f, 110.25f, -288.78f);
                break;
        }
        return position;
    }

  
    
   
    
}
