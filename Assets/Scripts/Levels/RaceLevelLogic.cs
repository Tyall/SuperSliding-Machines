using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceLevelLogic : MonoBehaviour
{
    public GameObject setFailMenu;
    public static GameObject failMenu;

    public int NumberOfCheckpoints;
    public int NumberOfEnemies;
    public int NumberOfLaps;
    public int GridPosition; //Make a functionality in vehicle spawning where the player pos will be changable (random), maybe rewards will depend from them
    public int CurrentLap;
    public int CurrentPosition;
    public int MinAmountOfCoins;
    public int MaxAmountOfCoins;
    public int MinAmountOfExp;
    public int MaxAmountOfExp;
    public static int numOfCheckpointsLeft;
    public static int totalCheckpointCount;
    public GameObject winMenu;
    public GameObject winCoins;
    public GameObject winExp;
    public int LevelNumber;
    public GameObject joystick;
    public GameObject countdownMenu;
    public TMPro.TextMeshProUGUI countdownText;

    public TMPro.TextMeshProUGUI playerPosition;
    public TMPro.TextMeshProUGUI playerPositionBG;
    public TMPro.TextMeshProUGUI playerLap;
    public TMPro.TextMeshProUGUI playerLapBG;
    public GameObject HUDPanel;



    bool isFinished = false;

    int coins;
    int experience;


    private void Start()
    {
        StartCoroutine(ShowCountdownMenu());
        CurrentLap = 1;
        CurrentPosition = GridPosition;
    }
    private void Awake()
    {
        failMenu = setFailMenu;
        numOfCheckpointsLeft = NumberOfCheckpoints;
        totalCheckpointCount = NumberOfCheckpoints;
        joystick.SetActive(true);
    }

    private void Update()
    {
        if (isFinished == false)
        {
            LevelFinished();
        }

    }

    public void UpdatePosition()
    {
        SetHUDText(1, "POSITION  " + CurrentLap + "  I  " + CurrentPosition);
    }

    public void UpdateLap()
    {
        SetHUDText(2, "LAP  "+ CurrentLap + "  I  " + NumberOfLaps);
    }

    public void SetHUDText(int mode,  string text)
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

    public static void ResetCheckpoints()
    {
        numOfCheckpointsLeft = totalCheckpointCount;
    }

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
        if (numOfCheckpointsLeft == 0)
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
        ResetCheckpoints(); //Probably doing a method in CheckpointLevelLogic-
        failMenu.SetActive(true);                                        //-that determines what level are we on is the best way to solve the problem
                                                                         //Find out a better way to determine which level do you currently play
                                                                         //Maybe do something like cpCountL+getCurrentLevel
    }

    public void LevelUnlocker()
    {
        if (LevelNumber == PlayerProfile.unlockedLevels)
        {
            PlayerProfile.unlockedLevels++;
        }
    }
}
