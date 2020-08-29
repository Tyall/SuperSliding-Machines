using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMovement : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    public GameObject GarageMenu;
    public GameObject ShopMenu;
    public GameObject MainMenu;
    public GameObject TapAnywhere;

    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    public void ViewGarage()
    {
        MainMenu.GetComponent<MainMenu>().HideLogo();
        anim.Play("main_to_garage");
        MainMenu.SetActive(false);
        TapAnywhere.SetActive(false);
    }
    public void OnViewGarageEnd()
    {
        GetComponent<GarageCameraMovement>().enabled = true;
        GarageMenu.SetActive(true);
    }
    public void ViewMenuFromGarage()
    {
        GetComponent<GarageCameraMovement>().enabled = false;
        MainMenu.GetComponent<MainMenu>().HideShopUI(); //rework in other cameras
        anim.Play("garage_to_main");
        GarageMenu.SetActive(false);
        
    }
    public void OnViewMenuFromGarageEnd()
    {
        MainMenu.GetComponent<MainMenu>().ShowLogo(); //there or on end of the animation
        MainMenu.SetActive(true);
        TapAnywhere.SetActive(true);
    }
    public void ViewMenuFromShop()
    {
        GetComponent<ShopCameraMovement>().enabled = false;
        MainMenu.GetComponent<MainMenu>().HideShopUI(); //rework in other cameras
        anim.Play("shop_to_main");
        ShopMenu.SetActive(false);
        MainMenu.GetComponent<MainMenu>().ShowLogo(); //there or on end of the animation
    }
    public void OnViewMenuFromShopEnd()
    {
       
        MainMenu.SetActive(true);
        TapAnywhere.SetActive(true);
    }

    
    public void ViewShop()
    {
        MainMenu.GetComponent<MainMenu>().HideLogo();
        anim.Play("main_to_shop");
        MainMenu.SetActive(false);
        TapAnywhere.SetActive(false);
    }
    public void OnViewShopEnd()
    {  
        GetComponent<ShopCameraMovement>().enabled = true;
        ShopMenu.SetActive(true);
    }
    


}
