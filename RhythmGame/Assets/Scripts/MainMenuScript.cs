using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void playPressed()
    {
        SceneManager.LoadScene("StageSelection");
    }

    public void settingsPressed()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void exitPress()
    {
        Application.Quit();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
