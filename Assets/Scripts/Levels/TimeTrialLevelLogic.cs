using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrialLevelLogic : MonoBehaviour
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
    public int LevelNumber;
    public GameObject joystick;
    bool isFinished = false;
    
    public GameObject countdownMenu;
    public TMPro.TextMeshProUGUI countdownText;

    int coins;
    int experience;

    private void Awake()
    {
        failMenu = setFailMenu;
        numOfCheckpointsLeft = NumberOfCheckpoints;
        totalCheckpointCount = NumberOfCheckpoints;
        joystick.SetActive(true);
    }

    void Update()
    {
        if (isFinished == false)
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

    

    public void LevelUnlocker()
    {
        if (LevelNumber == PlayerProfile.unlockedLevels)
        {
            PlayerProfile.unlockedLevels++;
        }
    }

    public static void TimeTrialLevelBehavior(string cpName)
    {
        Debug.Log("Entered " + cpName);
        GameObject.Find(cpName).SetActive(false);
        numOfCheckpointsLeft--;
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

        //HUDPanel.SetActive(true);
    }

    public void MeasureTIme()
    {

    }

    public void SaveBestTime()
    {

    }
}
