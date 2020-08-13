using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCameraMovement : MonoBehaviour
{
    public static int currentCameraFocus = -1;
    public GameObject shopFunction;
    public GameObject shopBack;
    public GameObject shopTopBack;
    public GameObject shopSelect;
    public GameObject shopTapAnywhere;
    TMPro.TextMeshProUGUI shopFunctionText;

    // Start is called before the first frame update
    void Start()
    {
        shopFunctionText = shopFunction.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCameraNext()
    {
        if (currentCameraFocus == -1 || currentCameraFocus == 2) // -1- main shop screen
        {
            currentCameraFocus = 0;  
            transform.position = new Vector3(46f, 111.5f, -269.2f);
            transform.rotation = new Quaternion(3.022f, 10f, 1.018f, 70f);
            shopFunctionText.text = "REPAINT VEHICLE";

        }
        else if (currentCameraFocus == 0)  // 0- repaint vehicle screen - zoom on vehicle in the garage
        {
            currentCameraFocus = 1;
            transform.position = new Vector3(41.6f, 111.6f, -264f);
            transform.rotation = new Quaternion(3.022f, 7f, 1.018f, 70f);
            shopFunctionText.text = "GET MORE COINS";
        }
        else if (currentCameraFocus == 1) // 1- premium goods
        {
            currentCameraFocus = 2;
            transform.position = new Vector3(36.6f, 113.3f, -233.4f);
            transform.rotation = new Quaternion(1.022f, 230f, -1.018f, 70f);
            shopFunctionText.text = "BUY NEW VEHICLE";
        }
       
    }

    public void MoveCameraPrevious()
    {
        
    }

    public void HideShopElements()
    {
        transform.position = new Vector3(43.4f, 113.4f, -280.75f);
        transform.rotation = new Quaternion(1.8f, 6.02f, -2.2f, 65f); //z -1.7 kinda worked except garage
        currentCameraFocus = -1;
        MainMenu.HideShopUI();
        shopTopBack.SetActive(true);
        shopTapAnywhere.SetActive(true);
    }
}
