using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckpointLevelLogic : MonoBehaviour
{

    public GameObject setFailMenu; 
    public static GameObject failMenu;

    public GameObject winMenu;
    public GameObject winCoins;
    public GameObject winExp;

    public static int cpCountL1 = 3;
    bool isFinished = false;
    
    int coins;
    int experience;

    private void Awake()
    {
        failMenu = setFailMenu;
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
        cpCountL1 = 3;
        //Maybe pass level number there and modify only one variable
    }

    void LevelFinished()
    {
        if (cpCountL1==0)
        {
            Debug.Log("Level Finished!"); //Show screen with rewards and restart/menu
            Time.timeScale = 0f;
            isFinished = true;
            GenerateRewards();
            winMenu.SetActive(true);
            Debug.Log("Finished Finishing xd");
        }
    }

    void GenerateRewards()
    {
        System.Random rnd = new System.Random();
        coins = rnd.Next(80, 120);

        experience = rnd.Next(20, 30);

        winCoins.GetComponent<TMPro.TextMeshProUGUI>().text = coins + "  COINS GAINED";
        winExp.GetComponent<TMPro.TextMeshProUGUI>().text = experience + "  EXPERIENCE GAINED";

        MainMenu.UpdateProfile(coins, experience);

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
