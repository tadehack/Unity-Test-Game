using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;

    [Space]

    [SerializeField] private PostProcessVolume postProcessVolume;
    private Bloom bloom;
    private MotionBlur motionBlur;
    private AmbientOcclusion ambientOcclusion;
        
    Resolution[] resolutions;

    private void Start()
    {
        //Post Processing
        postProcessVolume.profile.TryGetSettings(out bloom);
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        postProcessVolume.profile.TryGetSettings(out ambientOcclusion);

        //Screen Resolution
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " - " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    //Settings
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log(volume) * 20);
    }

    //Settings - Video
    public void SetVideoMode(int vidMode)
    {
        if (vidMode == 0)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVsync(bool boxChecked)
    {
        if (boxChecked)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

    //Settings - Graphics
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetBloom(bool boxChecked)
    {
        bloom.active = boxChecked;
    }

    public void SetMotionBlur(bool boxChecked)
    {
        motionBlur.active = boxChecked;
    }

    public void SetAmbientOcclusion(bool boxChecked)
    {
        ambientOcclusion.active = boxChecked;
    }
}
