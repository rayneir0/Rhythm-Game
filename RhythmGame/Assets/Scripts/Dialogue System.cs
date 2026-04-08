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
    private static string[][] stageText = new string[4][];
    public TextMeshProUGUI textBox;
    private int counter = 0;

    void Start()
    {
        stageText[0] = dialogue1.text.Split('\n');
        stageText[1] = dialogue2.text.Split('\n');
        stageText[2] = dialogue3.text.Split('\n');
        stageText[3] = dialogue4.text.Split('\n');


        
        if (textBox == null) // Prep dialogue once.
        {
            stageText[0] = dialogue1.text.Split('\n');
            stageText[1] = dialogue2.text.Split('\n');
            stageText[2] = dialogue3.text.Split('\n');
            stageText[3] = dialogue4.text.Split('\n');

        } else
        {
            textBox.text = stageText[stageNumber][0];
            counter++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox != null)
        {
            if (Input.GetMouseButtonDown(0) && counter == stageText[stageNumber].Length)
            {
                SceneManager.LoadScene("MainScene");
            }
            else if (Input.GetMouseButtonDown(0))
            {
                textBox.text = stageText[stageNumber][counter];
                counter++;
            }
        }
 
    }

    public void setStageNumber(int number)
    {
        stageNumber = number;
    }
}
