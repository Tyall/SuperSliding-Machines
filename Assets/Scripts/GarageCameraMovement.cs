using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageCameraMovement : MonoBehaviour
{

    public int currentCameraFocusPlace = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveCameraNext()
    {
        if (currentCameraFocusPlace < 10)
        {
            //Camera movement code
            currentCameraFocusPlace++;
        }
        
    }

    void MoveCameraPrevious()
    {
        if (currentCameraFocusPlace > 0)
        {
            //Camera movement code
            currentCameraFocusPlace --;
        }
    }
}
