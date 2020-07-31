using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int playerLevel;
    public int playerCoins;
    public int playerExp;
    public int unlockedLevels;
    
    
    public PlayerData ()
    {
        playerName = PlayerProfile.playerName;
        playerLevel = PlayerProfile.playerLevel;
        playerCoins = PlayerProfile.playerCoins;
        playerExp = PlayerProfile.playerExp;
        unlockedLevels = PlayerProfile.unlockedLevels;
    }

}
