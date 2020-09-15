using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
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

    public Vector3 StartingPosition;
    public Quaternion StartingRotation;

    int vehID = PlayerProfile.ownedVehicles[0];
    int vehColor = PlayerProfile.ownedVehiclesColors[0];
    static Material[] materialsArray;
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        CreateVehicle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateVehicle()
    {
        switch (vehID)
        {

            case 1:
                var newObject = (GameObject)Instantiate(sedan, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;
            case 2:
                newObject = (GameObject)Instantiate(sedanSport, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;
            case 3:
                newObject = (GameObject)Instantiate(suv, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;
            case 4:
                newObject = (GameObject)Instantiate(hatchbackSport, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;
            case 5:
                newObject = (GameObject)Instantiate(suvLuxury, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;
            case 6:
                newObject = (GameObject)Instantiate(truck, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;
            case 7:
                newObject = (GameObject)Instantiate(van, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;
            case 8:
                newObject = (GameObject)Instantiate(police, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;
            case 9:
                newObject = (GameObject)Instantiate(race, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;
            case 10:
                newObject = (GameObject)Instantiate(raceFuture, StartingPosition, StartingRotation);
                newObject.name = ("PlayerVehicle");
                AssignVehicleColor(newObject, vehID);
                break;

        }
    }

    void AssignVehicleColor(GameObject target, int vehID)
    {
        materialsArray = target.GetComponentInChildren<MeshRenderer>().materials;

        if (vehID == 2)
        {
            Transform vehicle = target.transform.Find("body");
            Transform spoiler = vehicle.transform.Find("spoiler");
            Material[] spoilerMaterialsArray;
            materialsArray[1] = AssignColor();
            spoilerMaterialsArray = spoiler.GetComponent<MeshRenderer>().materials;
            spoilerMaterialsArray[1] = AssignColor();
            spoiler.GetComponent<MeshRenderer>().materials = spoilerMaterialsArray;

        }
        else if (vehID == 9)
        {
            materialsArray[0] = AssignColor();
        }
        else
        {
            materialsArray[1] = AssignColor();
        }

        target.GetComponentInChildren<MeshRenderer>().materials = materialsArray;

    }

    Material AssignColor()
    {
        switch (vehColor)
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
}
