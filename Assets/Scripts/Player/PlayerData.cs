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
    public int[] ownedVehicles;
    public int[] ownedVehiclesColors;


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
