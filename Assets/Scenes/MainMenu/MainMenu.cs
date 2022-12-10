using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private protected Dropdown dropResolutions, dropQualitys;
    [SerializeField] private protected Toggle toggleWindow;

    private List<string> resolutions = new List<string>();
    private List<string> qualitys = new List<string>();

    private void Start() {
        dropResolutions.ClearOptions();
        dropQualitys.ClearOptions();
         
        Resolution[] arrResolutions = Screen.resolutions;
        foreach(Resolution r in arrResolutions) resolutions.Add(string.Format("{0} X {1}", r.width, r.height));

        dropResolutions.AddOptions(resolutions);
        dropResolutions.value = (resolutions.Count - 1);

        qualitys = QualitySettings.names.ToList<string>();
        dropQualitys.AddOptions(qualitys);
        dropQualitys.value = QualitySettings.GetQualityLevel();
    }

    public void SetResolution() {
        string[] res = resolutions[dropResolutions.value].Split('X');
        Screen.SetResolution(Convert.ToInt16(res[0].Trim()), Convert.ToInt16(res[1].Trim()), Screen.fullScreen);
    }
    public void SetQuality() {
        QualitySettings.SetQualityLevel(dropQualitys.value, true);
    }

    public void SetWindowMode() {
        if(toggleWindow.isOn) Screen.fullScreen = false; 
        else Screen.fullScreen = true;
    }

    public void StartGame() {
        SceneManager.LoadSceneAsync(2);
    }

    public void Quit() {
        Application.Quit();
    }
}