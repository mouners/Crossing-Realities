using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public GameObject CameraHolder;
    public GameObject Player;
    public GameObject GameManager;

    public GameObject MenuPanel;
    public GameObject Camera;

    public Dropdown ResDropDown;
    public Toggle FullScreenToggle;

    Resolution[] AllResolution;
    bool IsFullScreen;
    int SelectedResolution;
    List<Resolution> SelectedResolutionList = new List<Resolution>();
    // Start is called before the first frame update
    void Start()
    {
        IsFullScreen = true;
        AllResolution = Screen.resolutions;

        List<string> resolutionStringList = new List<string>();
        string newRes;
        foreach(Resolution resolution in AllResolution){
            newRes = resolution.width.ToString() + " x "+ resolution.height.ToString();
            if(!resolutionStringList.Contains(newRes)){
                resolutionStringList.Add(newRes);
                SelectedResolutionList.Add(resolution);
            }
        }
        ResDropDown.AddOptions(resolutionStringList);
    }

    public void ChangeResolution(){
        SelectedResolution = ResDropDown.value;
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, IsFullScreen);
    }

    public void ChangeFullScreen(){
        IsFullScreen = FullScreenToggle.isOn;
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, IsFullScreen);
    }

    public void OnPlayBtnClicked(){
        MenuPanel.gameObject.SetActive(false);
        Camera.gameObject.SetActive(false);
        CameraHolder.gameObject.SetActive(true);
        Player.gameObject.SetActive(true);
        GameManager.gameObject.SetActive(true);
    }

    public void OnExitBtnClicked(){
        Application.Quit();
    }
}
