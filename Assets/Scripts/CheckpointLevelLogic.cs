using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckpointLevelLogic : MonoBehaviour
{
    

    public GameObject setFailMenu; 
    public static GameObject failMenu;

    public int NumberOfCheckpoints;
    public int MinAmountOfCoins;
    public int MaxAmountOfCoins;
    public int MinAmountOfExp;
    public int MaxAmountOfExp;
    public static int numOfCheckpointsLeft;
    public static int totalCheckpointCount;
    public GameObject winMenu;
    public GameObject winCoins;
    public GameObject winExp;

    
    bool isFinished = false;
    
    int coins;
    int experience;

    private void Awake()
    {
        failMenu = setFailMenu;
        numOfCheckpointsLeft = NumberOfCheckpoints;
        totalCheckpointCount = NumberOfCheckpoints;
    }

    private void Update()
    {
        if (isFinished==false)
        {
            LevelFinished();
        }
    
    }

    public static void ResetCheckpoints()
    {
        numOfCheckpointsLeft = totalCheckpointCount;
    }

    void LevelFinished()
    {
        if (numOfCheckpointsLeft == 0)
        {
            Debug.Log("Level Finished!"); 
            Time.timeScale = 0f;
            isFinished = true;
            GenerateRewards();
            winMenu.SetActive(true);

            SaveSystem.SavePlayer();
        }
    }

    void GenerateRewards()
    {
        System.Random rnd = new System.Random();
        coins = rnd.Next(MinAmountOfCoins, MaxAmountOfCoins);

        experience = rnd.Next(MinAmountOfExp, MaxAmountOfExp);

        winCoins.GetComponent<TMPro.TextMeshProUGUI>().text = coins + "  COINS GAINED";
        winExp.GetComponent<TMPro.TextMeshProUGUI>().text = experience + "  EXPERIENCE GAINED";

        PlayerProfile.UpdateProfile(coins, experience);

    }

    public static void LevelFailed()
    {
        
        Debug.Log("You hit the traffic cone!");
        Time.timeScale = 0f;
        ResetCheckpoints(); //Probably doing a method in CheckpointLevelLogic-
        failMenu.SetActive(true);                                        //-that determines what level are we on is the best way to solve the problem
                                                                         //Find out a better way to determine which level do you currently play
                                                                         //Maybe do something like cpCountL+getCurrentLevel
    }
}
