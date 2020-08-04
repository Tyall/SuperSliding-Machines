using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 180f;

    bool moveToGarage = false;
    bool moveToMenuFromGarage = false;
    bool moveToShop = false;
    bool moveToMenuFromShop = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
               
        if (moveToGarage == true)
            {
                if (transform.rotation.y < 0.89)
                {
                    transform.Rotate(0, Time.deltaTime * movementSpeed, 0);
                }
                else moveToGarage = false;
              
            }


        if (moveToMenuFromGarage == true)
        {
            if (transform.rotation.y > 0.525)
            {
                transform.Rotate(0, Time.deltaTime * -movementSpeed, 0);
            }
            else moveToMenuFromGarage = false;

        }

        if (moveToShop == true)
        {
            if (transform.rotation.y > 0.10)
            {
                transform.Rotate(0, Time.deltaTime * -movementSpeed, 0);
            }
            else moveToShop = false;

        }


        if (moveToMenuFromShop == true)
        {
            if (transform.rotation.y < 0.515)
            {
                transform.Rotate(0, Time.deltaTime * movementSpeed, 0);
            }
            else moveToMenuFromShop = false;

        }


    }

    public void ViewGarage()
    {
        moveToGarage = true;
    }
    public void ViewMenuFromGarage()
    {
        moveToMenuFromGarage = true;
    }
    public void ViewShop()
    {
        moveToShop = true;
    }
    public void ViewMenuFromShop()
    {
        moveToMenuFromShop = true;
    }
    

}
