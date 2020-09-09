using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static bool isFirstStartup; //Not working as intended. Find another way to handle it

    public static string playerName;
    public static int playerLevel;
    public static int playerExp;

    //public static int playerCoins = 34000;
    public static int playerCoins;
    // public static int unlockedLevels = 3; //Temporary solution. Completing a N level will unlock N+1 level
    public static int unlockedLevels;
    //public static int[] ownedVehicles = new int[11] {8, 1, 2, 3, 4, 0, 0, 0, 0, 0, 10};
    public static int[] ownedVehicles;
    //public static int[] ownedVehiclesColors = new int[11] {3, 6, 5, 7, 6, 0, 0, 0, 0, 0, 7};
    public static int[] ownedVehiclesColors;
    
    

    public GameObject GManager;

    //Explanation of the ownedVehicles arrays: First element is the CURRENT CAR, next 10 elements are parking lots. Player can have 11 cars in total
    //Temporary solution. Player will start with a sedan or perhaps 1000 coins and the tutorial will force player to buy a vehicle and maybe repaint it (1000 coins + repaint cost)
    //public static bool isTutorial;




    //Create a function which will check if you can buy another veh; checking when trying to buy a vehicle
    //Following - make an option to sell a vehicle, maybe add it to Repaint or Vehicle Buy Menu 

    //TODO : clear owned vehicles and make owned vehicles along with selectedVehicle be saved to a binary file

    public void Start()
    {
        LoadPlayer();

        //DebugResetFirstTimeOpening();

        FirstStartupSetupTest();

        GManager.GetComponent<GarageManager>().UpdateVehicles();

    }

    public void DebugResetFirstTimeOpening()
    {
        PlayerPrefs.SetInt("FIRSTTIMEOPENING", 1);
        Debug.Log("First time opening flag reseted");
    }
    public void FirstStartupSetupTest()
    {

          if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
          {
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
            playerCoins = 35000;
            unlockedLevels = 3;
            ownedVehicles = new int[11] {2, 4, 7, 0, 0, 0, 0, 0, 0, 0, 0};
            ownedVehiclesColors = new int[11] {7, 5, 2, 0, 0, 0, 0, 0, 0, 0, 0};


            SaveSystem.SavePlayer();
        }
        else
        {
            
            Debug.Log("Not First Time Opening");
        }

        
        

        
        
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
        ownedVehicles = data.ownedVehicles;
        ownedVehiclesColors = data.ownedVehiclesColors;

        MainMenu.UpdateUI(); 
    }

}
