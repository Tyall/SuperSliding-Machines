using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool isFirstStartup;
    public string playerName;
    public int playerLevel;
    public int playerCoins;
    public int playerExp;
    public int unlockedLevels;
    public int[] ownedVehicles = new int[11];
    public int[] ownedVehiclesColors = new int[11];


    public PlayerData ()
    {
        isFirstStartup = PlayerProfile.isFirstStartup;
        playerName = PlayerProfile.playerName;
        playerLevel = PlayerProfile.playerLevel;
        playerCoins = PlayerProfile.playerCoins;
        playerExp = PlayerProfile.playerExp;
        unlockedLevels = PlayerProfile.unlockedLevels;
        ownedVehicles = PlayerProfile.ownedVehicles;
        ownedVehiclesColors = PlayerProfile.ownedVehiclesColors;
    }

}
