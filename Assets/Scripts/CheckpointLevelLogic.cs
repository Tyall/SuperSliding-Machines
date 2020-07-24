using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLevelLogic : MonoBehaviour
{

    public static int cpCountL1 = 3;
    bool isFinished = false;
    // Start is called before the first frame update

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
        }
    }
}
