using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceLevelLogic : MonoBehaviour
{
    public GameObject setFailMenu;
    public static GameObject failMenu;

    [InspectorName("Level Parameters")]
    public int NumberOfCheckpoints;
    public int NumberOfEnemies;
    public int NumberOfLaps;
    public int GridPosition; //Make a functionality in vehicle spawning where the player pos will be changable (random), maybe rewards will depend from them
    public int CurrentLap;
    public int CurrentPosition;
    public int CheckpointsPerLap;
    public int MinAmountOfCoins;
    public int MaxAmountOfCoins;
    public int MinAmountOfExp;
    public int MaxAmountOfExp;

    [HideInInspector]
    public static bool raceFinished;
    public static int totalCheckpointsPassed;
    public static int cpPerLap;
    public static int cpPerLapPassed;
    public static int totalLaps;
    public static int lapsLeft;
    public static int currentLap;
    public static int currentPosition;
    public static int numberOfEnemies;
    public static GameObject lastCheckpoint;

    [InspectorName("GameObjects")]
    public GameObject winMenu;
    public GameObject winCoins;
    public GameObject winExp;
    public int LevelNumber;
    public GameObject joystick;
    public GameObject countdownMenu;
    public TMPro.TextMeshProUGUI countdownText;

    public TMPro.TextMeshProUGUI PlayerPosition;
    public static TMPro.TextMeshProUGUI playerPosition;
    public TMPro.TextMeshProUGUI PlayerPositionBG;
    public static TMPro.TextMeshProUGUI playerPositionBG;
    public TMPro.TextMeshProUGUI PlayerLap;
    public static TMPro.TextMeshProUGUI playerLap;
    public TMPro.TextMeshProUGUI PlayerLapBG;
    public static TMPro.TextMeshProUGUI playerLapBG;
    public GameObject HUDPanel;
    public GameObject LastCheckpoint;


    bool isFinished = false;

    int coins;
    int experience;


    private void Start()
    {
        StartCoroutine(ShowCountdownMenu());
        CurrentLap = 1;
        CurrentPosition = GridPosition;
        totalLaps = NumberOfLaps;
        cpPerLap = CheckpointsPerLap;
        currentLap = CurrentLap;
        cpPerLapPassed = 0;
        lastCheckpoint = LastCheckpoint;
        numberOfEnemies = NumberOfEnemies;
        currentPosition = CurrentPosition;


        playerPosition = PlayerPosition;
        playerPositionBG = PlayerPositionBG;
        playerLap = PlayerLap;
        playerLapBG = PlayerLapBG;

        UpdateHUD();
    }
    private void Awake()
    {
        failMenu = setFailMenu;
        joystick.SetActive(true);
    }

    private void Update()
    {
        if (isFinished == false)
        {
            LevelFinished();
        }


    }
   

   public static void UpdateHUD()
    {
       UpdatePosition();
       UpdateLap();
    }

    public static void UpdatePosition()
    {
        print("current pos: " + currentPosition);
        SetHUDText(1, "POSITION  " + currentPosition + "  I  " + (numberOfEnemies+1));
    }

    public static void UpdateLap()
    {
        SetHUDText(2, "LAP  "+ currentLap + "  I  " + totalLaps);
    }

    public static void SetHUDText(int mode,  string text)
    {
        switch (mode)
        {
            case 1:
                playerPosition.text = text;
                playerPositionBG.text = text;
                break;
            case 2:
                playerLap.text = text;
                playerLapBG.text = text;
                break;
        }
        
    }

    /*public static void ResetCheckpoints()
    {
        numOfCheckpointsLeft = totalCheckpointCount;
    }*/

    IEnumerator ShowCountdownMenu()
    {
        Time.timeScale = 0;
        countdownText.text = "GET READY";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "GO";
        yield return new WaitForSecondsRealtime(0.2f);
        countdownMenu.active = false;
        Time.timeScale = 1;

        HUDPanel.SetActive(true);
    }
    void LevelFinished()
    {
        if (raceFinished == true)
        {
            Debug.Log("Level Finished!");
            joystick.SetActive(false);
            Time.timeScale = 0f;
            isFinished = true;
            GenerateRewards();
            winMenu.SetActive(true);
            LevelUnlocker();
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
        //Time.timeScale = 0f;
        
        failMenu.SetActive(true);                                        
    }

    public void LevelUnlocker()
    {
        if (LevelNumber == PlayerProfile.unlockedLevels)
        {
            PlayerProfile.unlockedLevels++;
        }
    }

    public static void RaceLevelBehavior(string cpName)
    {
        Debug.Log("Entered " + cpName);
        //GameObject.Find(cpName).SetActive(false);
        if (cpPerLapPassed < (cpPerLap-1))
        {
            cpPerLapPassed++;
            totalCheckpointsPassed++;
            if (cpPerLapPassed == 10)
            {
                lastCheckpoint.SetActive(true); //reactivate last checkpoint around the middle of the race
            } 
        }
        else if (cpPerLapPassed == (cpPerLap-1))
        {
            HandleLaps();
        }
        UpdateHUD();
    }

    public static void HandleLaps()
    {
        Debug.Log("Lap + " + currentLap + " completed");
        cpPerLapPassed = 0;
        
        
        if (currentLap == totalLaps)
        {
            raceFinished = true;
        }
        else
        {
            currentLap++;
        }
        
    }
    
}
