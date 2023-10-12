using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings_Script : MonoBehaviour
{

    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    //LeanLocalization[] languages;


    //public TMPro.TMP_Dropdown languagesDropdown;
    
    ////LeanLocalization GameObject in Scene to get all languages
    //public LeanLocalization leanLocalization;

    void Start()
    {
        //creating new options for UI dropdown menu
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
            
        //creo una lista con las resoluciones
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            //formateo el string para que Resolutions lo acepte.
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width &&
               resolutions[i].height == Screen.height )
            {
                currentResolutionIndex = i;
            }

        }

        //le paso la nueva opción
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        



    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Setting volume with a UI Slider
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    //Setting quality with a dropdown menu in UI
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //setting fullscreen
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
