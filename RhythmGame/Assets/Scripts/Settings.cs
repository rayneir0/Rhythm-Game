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
    public AudioSource menuAudio;
    public Slider songVolumeSlider;
    public Slider bgVolumeSlider;
    public Slider mainMenuVolumeSlider;
    private float prevVolume = 1f;
    private static float crowdVolume = 1f;
    private static float songVolume = 1f;
    private static float menuVolume = 1f;

    void Start()
    {
        songVolumeSlider.value = songVolume;
        bgVolumeSlider.value = crowdVolume;
        if(mainMenuVolumeSlider != null)
        {
            mainMenuVolumeSlider.value = menuVolume;
            mainMenuVolumeSlider.onValueChanged.AddListener(SetMenuVolume);
        }

        songVolumeSlider.onValueChanged.AddListener(SetSongVolume);
        bgVolumeSlider.onValueChanged.AddListener(SetBGVolume);
        
        if (songManager != null)
        {
            audioSource = songManager.audioSource;
            audioSource.volume = songVolume;
        }

        if (backgroundNoise != null)
        {
            backgroundNoise.volume = crowdVolume;
        }

        if (menuAudio != null)
        {
            menuAudio.volume = menuVolume;
        }


        /*
        audioSource = songManager.audioSource;

        songVolumeSlider.value = audioSource.volume;
        bgVolumeSlider.value = backgroundNoise.volume;

        songVolumeSlider.onValueChanged.AddListener(SetSongVolume);
        bgVolumeSlider.onValueChanged.AddListener(SetBGVolume);
        */

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
        //audioSource.volume = newVolume;
        if (audioSource != null)
        {
            audioSource.volume = newVolume;
        }
        songVolume = newVolume;
    }

    public void SetBGVolume(float newVolume)
    {
        //backgroundNoise.volume = newVolume;
        if (backgroundNoise != null)
        {
            backgroundNoise.volume = newVolume;
        }
        crowdVolume = newVolume;
    }

    public void SetMenuVolume(float newVolume)
    {
        menuVolume = newVolume;
        if (menuAudio != null)
        {
            menuAudio.volume = menuVolume;
        }
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

    public void pauseGame()
    {
        Time.timeScale = 0.00001f;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }
}
