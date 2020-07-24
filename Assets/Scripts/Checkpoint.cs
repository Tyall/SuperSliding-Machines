using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void CheckpointBehavior(string cpName)
    {
        Debug.Log("Entered " +cpName);
        GameObject.Find(cpName).SetActive(false);
        CheckpointLevelLogic.cpCountL1--;
    }
}
