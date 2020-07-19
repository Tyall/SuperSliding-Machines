using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private PostProcessVolume postProcessVolume;
    

    public void PlayGame()
    {
        AnimateLogo();
        ShowLevelSelection();
    }

    public void SelectLevel()
    {
        //case level 1:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //case level 2:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void AnimateLogo()
    {
        GameObject.Find("Logo").LeanMoveLocalY(750, 0.5f);
    }

    public void ShowLevelSelection()
    {
        //OPTIMISE THE CODE | SHOW n HIDE

        GameObject.Find("LevelSelectionBG").LeanMoveLocalY(100, 0.7f);

        Camera cam = GameObject.Find("Camera").GetComponent<Camera>();

        postProcessVolume = cam.GetComponent<PostProcessVolume>();

        ChangeDepthOfField(0.1f);
        

       //dof aperture value has to come back to 4.1 when the menu closes or the main menu screen is no longer visible
    }

    public void ChangeDepthOfField(float value)
    {
        var volume = this.postProcessVolume;

        var profile = volume.sharedProfile;

        var depthOfField = profile.GetSetting<DepthOfField>();

        depthOfField.aperture.value = value;
        //make it progressive?
    }

    public void HideLevelSelection()
    {
        GameObject.Find("LevelSelectionBG").LeanMoveLocalY(1000, 0.7f);

        Camera cam = GameObject.Find("Camera").GetComponent<Camera>();

        postProcessVolume = cam.GetComponent<PostProcessVolume>();

        ChangeDepthOfField(4.1f);

    }
    
}
