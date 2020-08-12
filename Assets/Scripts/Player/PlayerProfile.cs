using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static string playerName;
    public static int playerLevel;
    public static int playerCoins;
    public static int playerExp;
    public static int unlockedLevels = 2;


    public static int[] ownedVehicles = new int[11] {8, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    public static int[] ownedVehiclesColors = new int[11] { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
    //Rebuilding: first element is the CURRENT CAR, next 10 elements are parking lots. 4th is empty, player can have 11 cars in total 
    
    
    
    //max 10, create a function which will check if you can buy another veh; checking when trying to buy a vehicle
    //also check if the user already has X type of vehicle so no dupes or maybe create a veh selling possibility
   
    

    //TODO : clear owned vehicles and make owned vehicles along with selectedVehicle be saved to a binary file

    //public int unlockedVehicles; - working as unlockedLevels, vehicles will be in order
    // - colors of the vehicles have to be saved also
    //to handle the color probably a new class called LoadPlayerProfile giving a car color dependent on cases will be needed


    public void Start()
    {
        LoadPlayer();
    }
    public static void UpdateProfile(int coin, int exp)
    {
        playerExp += exp;
        playerCoins += coin;

    }

    public void SavePlayer()
    {
        Debug.Log("SavePlayer executed");
        SaveSystem.SavePlayer();
    }

    public void LoadPlayer()
    {
        Debug.Log("LoadPlayer executed");
        PlayerData data = SaveSystem.LoadPlayer();

        playerName = data.playerName;
        playerLevel = data.playerLevel;
        playerCoins = data.playerCoins;
        playerExp = data.playerExp;
        unlockedLevels = data.unlockedLevels;

        MainMenu.UpdateUI(); //For debug purposes. UI will be updated automatically on player profile loaded - on app launched
    }

}
