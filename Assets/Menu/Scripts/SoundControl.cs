using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{

    [SerializeField]
    private Slider mainVolumeSlider;

    [SerializeField]
    private Toggle mainMute;

    [SerializeField]
    private Slider songVolumeSlider;

    [SerializeField]
    private Toggle songMute;

    [SerializeField]
    private Slider FXVolumeSlider;

    [SerializeField]
    private Toggle FXMute;

    [SerializeField]
    AudioMixer MasterMixer;

    public void ChangeSound()
    {
        
        if (mainMute.GetComponent<Toggle>().isOn)
        {
            MasterMixer.SetFloat("masterVolume", -80);
        }
        else
        {
            MasterMixer.SetFloat("masterVolume", mainVolumeSlider.value);
        }

        if (songMute.GetComponent<Toggle>().isOn)
        {
            MasterMixer.SetFloat("musicVolume", -80);
        }
        else
        {
            MasterMixer.SetFloat("musicVolume", songVolumeSlider.value);
        }

        if (FXMute.GetComponent<Toggle>().isOn)
        {
            MasterMixer.SetFloat("fxVolume", -80);
        }
        else
        {
            MasterMixer.SetFloat("fxVolume", FXVolumeSlider.value);
        }


    }
}
