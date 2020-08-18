using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static bool isFirstStartup; //Not working as intended. Find another way to handle it

    public static string playerName;
    public static int playerLevel;
    public static int playerCoins;
    public static int playerExp;

    public static int unlockedLevels = 2; //Temporary solution. Completing a N level will unlock N+1 level

    public static int[] ownedVehicles = new int[11] {8, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    public static int[] ownedVehiclesColors = new int[11] {3, 6, 5, 7, 6, 2, 10, 1, 4, 2, 7};

    //public static int unlockedLevels;
    //public static int[] ownedVehicles = new int[11];
    //public static int[] ownedVehiclesColors = new int[11];

    //Explanation of the ownedVehicles arrays: First element is the CURRENT CAR, next 10 elements are parking lots. Player can have 11 cars in total
    //Temporary solution. Player will start with a sedan or perhaps 1000 coins and the tutorial will force player to buy a vehicle and maybe repaint it (1000 coins + repaint cost)
    //public static bool isTutorial;





    //Create a function which will check if you can buy another veh; checking when trying to buy a vehicle
    //Following - make an option to sell a vehicle, maybe add it to Repaint or Vehicle Buy Menu 

    //TODO : clear owned vehicles and make owned vehicles along with selectedVehicle be saved to a binary file

    public void Start()
    {
        LoadPlayer();

        //Code below isn't working
        if (isFirstStartup){
            FirstStartupSetupTest();
        }
        
    }

    public void FirstStartupSetupTest()
    {
        //Not working
        unlockedLevels = 3;
        ownedVehicles[0] = 1;
        ownedVehiclesColors[0] = 3;
        isFirstStartup = false;
        Debug.Log("First Startup");
        SavePlayer();
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
        //ownedVehicles = data.ownedVehicles;
        //ownedVehiclesColors = data.ownedVehiclesColors;

        MainMenu.UpdateUI(); 
    }

}
