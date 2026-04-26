using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static int stageNumber = -1;
    public TextAsset dialogue1;
    public TextAsset dialogue2;
    public TextAsset dialogue3;
    public TextAsset dialogue4;
    public TextAsset End1G;
    public TextAsset End1B;
    /*
    public TextAsset End2G;
    public TextAsset End2B;
    public TextAsset End3G;
    public TextAsset End3B;
    public TextAsset End4G;
    public TextAsset End4B;
    */
    private static string[][] stageText = new string[4][];
    private static string[][] endText = new string[2][];
    public TextMeshProUGUI textBox;
    private int counter = 0; // Text counter
    private static bool endStage = false;
    private static bool passStatus = false;
    private int length;

    void Start()
    {
        /*
        stageText[0] = dialogue1.text.Split('\n');
        stageText[1] = dialogue2.text.Split('\n');
        stageText[2] = dialogue3.text.Split('\n');
        stageText[3] = dialogue4.text.Split('\n');
        endText[0] = End1G.text.Split("\n");
        endText[1] = End1B.text.Split("\n");
        */



        if (textBox == null) // Prep dialogue once.
        {
            stageText[0] = dialogue1.text.Split('\n');
            stageText[1] = dialogue2.text.Split('\n');
            stageText[2] = dialogue3.text.Split('\n');
            stageText[3] = dialogue4.text.Split('\n');
            endText[0] = End1G.text.Split("\n");
            endText[1] = End1B.text.Split("\n");

        } else if (endStage) // It's an ending dialogue
        {
            if (passStatus) // Player completed the stage well enough
            {
                textBox.text = endText[0][0];
                length = endText[0].Length;
                counter++;
            } 
            else // player failed stage.
            {
                textBox.text = endText[1][0];
                length = endText[1].Length;
                counter++;
            }
            
        } else
        {
            textBox.text = stageText[stageNumber][0];
            length = stageText[stageNumber].Length;
            counter++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox != null)
        {
            if (Input.GetMouseButtonDown(0) && counter == length && endStage)
            {
                endStage = false;
                passStatus = false;
                SceneManager.LoadScene("Main Menu");
                
            }
            else if (Input.GetMouseButtonDown(0) && counter == length)
            {
                SceneManager.LoadScene("MainScene");
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(counter);
                Debug.Log(length);
                if (endStage)
                {
                    if (passStatus)
                    {
                        textBox.text = endText[0][0];
                    } 
                    else
                    {
                        textBox.text = endText[1][0];
                    }
                }
                else
                {
                    textBox.text = stageText[stageNumber][counter];
                }
                counter++;
            }
        }
 
    }

    public void setEndTrue()
    {
        endStage = true;
    }

    public void setStageNumber(int number)
    {
        stageNumber = number;
    }

    public void setCompletion()
    {
        passStatus = true;
    }
}
