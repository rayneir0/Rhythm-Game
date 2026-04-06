using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject settingsPanel;

    public SongManager songManager;

    public AudioSource audioSource;
    public AudioSource backgroundNoise;
    public Slider songVolumeSlider;
    public Slider bgVolumeSlider;
    private float prevVolume = 1f;

    void Start()
    {
        audioSource = songManager.audioSource;

        songVolumeSlider.value = audioSource.volume;
        bgVolumeSlider.value = backgroundNoise.volume;

        songVolumeSlider.onValueChanged.AddListener(SetSongVolume);
        bgVolumeSlider.onValueChanged.AddListener(SetBGVolume);
        

    }
    public void ShowPanel()
    {
        if(settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }
    public void HidePanel()
    {
        if (settingsPanel != null)
        {
            settingsPanel.gameObject.SetActive(false);
        }
    }

    public void SetSongVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }

    public void SetBGVolume(float newVolume)
    {
        backgroundNoise.volume = newVolume;
    }
    public void ToggleMute(AudioSource audio)
    {

        if(audio.volume > 0f)
        {
            prevVolume = audio.volume;
            audio.volume = 0f;
        }
        else
        {
            audio.volume = prevVolume;
        }
    }
}
