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
    //public static int ownedVehicles[];

    //public int unlockedVehicles; - working as unlockedLevels, vehicles will be in order
    //public string ownedVehicles[hatchbackSports, taxiVehicle]; - colors of the vehicles have to be saved also
    //to handle the color probably a new class called LoadPlayerProfile giving a car color dependent on cases will be needed
    //public string selectedVehicle; - currently selected vehicle

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
