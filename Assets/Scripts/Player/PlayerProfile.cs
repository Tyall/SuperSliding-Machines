using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerProfile : MonoBehaviour
{
    public static bool isFirstStartup; //Not working as intended. Find another way to handle it

    public static string playerName;
    public static int playerLevel;
    public static int playerExp;

    //public static int playerCoins = 34000;
    public static int playerCoins;
    public static int unlockedLevels = 6; //Temporary solution. Completing a N level will unlock N+1 level
    //public static volatile int unlockedLevels ;
    public static int[] ownedVehicles = new int[11] {8, 1, 2, 3, 4, 0, 0, 0, 0, 0, 10};
    //public static int[] ownedVehicles;
    public static int[] ownedVehiclesColors = new int[11] {9, 6, 5, 7, 6, 0, 0, 0, 0, 0, 7};
    //public static int[] ownedVehiclesColors;

    public TextMeshProUGUI debugobject;
    
    

    public GameObject GManager;

    //Explanation of the ownedVehicles arrays: First element is the CURRENT CAR, next 10 elements are parking lots. Player can have 11 cars in total
    //Temporary solution. Player will start with a sedan or perhaps 1000 coins and the tutorial will force player to buy a vehicle and maybe repaint it (1000 coins + repaint cost)
    //public static bool isTutorial;




    //Create a function which will check if you can buy another veh; checking when trying to buy a vehicle
    //Following - make an option to sell a vehicle, maybe add it to Repaint or Vehicle Buy Menu 

    //TODO : clear owned vehicles and make owned vehicles along with selectedVehicle be saved to a binary file

    public void Start()
    {
        StartupSetup();

        GManager.GetComponent<GarageManager>().UpdateVehicles();

    }

    public void StartupSetup()
    {
        //Directly test first time setup
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENINGTEST4", 1) == 1)
        {
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENINGTEST4", 0);
            playerCoins = 500;
            unlockedLevels = 1;
            ownedVehicles = new int[11] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            ownedVehiclesColors = new int[11] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            SaveSystem.SavePlayer();
            LoadPlayer();
            MainMenu.UpdateUI();
        }
        else
        {
            LoadPlayer();
            Debug.Log("Not First Time Opening");
        }
        //end of test

    }
    public static void UpdateProfile(int coin, int exp)
    {
        playerExp += exp;
        playerCoins += coin;

    }

    public void debugFunction()
    {
        debugobject.text = "UNLOCKED LEVELS " + unlockedLevels;
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
        unlockedLevels = data.unlockedLevels; //DEBUGO
        ownedVehicles = data.ownedVehicles;
        ownedVehiclesColors = data.ownedVehiclesColors;

        MainMenu.UpdateUI(); 
    }

}
