using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    private static int stagesCompleted = 0;
    public GameObject stageGrid;
    public SongManager songManager;
    public DialogueSystem Dialogue;


    // Start is called before the first frame update
    void Start()
    {
        showStages();
    }

    // Update is called once per frame
    void Update()
    {
        if (stageGrid.activeSelf && Input.GetKeyDown("u"))
        {
            masterMode();
            //Debug.Log("Master Mode");
        }
    }

    void masterMode()
    {
        //Debug.Log(stageGrid.transform.childCount);
        for (int i = 0; i < stageGrid.transform.childCount; i++)
        {
            stageGrid.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void showStages()
    {
        if (stagesCompleted > 3)
        {
            stagesCompleted = 3;
        }
        for (int i = 0; i < stagesCompleted+1; i++)
        {
            stageGrid.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void loadStage(int number)
    {
        songManager.setIsMenu(false);
        Dialogue.setStageNumber(number);
        SceneManager.LoadScene("Dialogue Scene");
    }

    public void stageComplete()
    {
        stagesCompleted++;
    }

 


}
