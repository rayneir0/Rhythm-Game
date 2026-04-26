using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CalculateScore : MonoBehaviour
{
    public int totalScore;
    public int currentCombo;
    // For testing
    public int perfectHit;
    public int greatHit;
    public int goodHit;
    public int badHit;
    public TextMeshProUGUI scoreText;
    public DialogueManager dialogueManager;
    public AmbienceManager ambienceManager;
    public StageSelect stageSelect;
    public DialogueSystem dialogue;
    public void AddScore(NoteController note)
    {
        switch (note.currentHitType)
        {   // Checks the current hit type of note, and gives it a score
            case HitType.Perfect:
                totalScore  += 20;
                currentCombo ++; // Adds to the combo
                perfectHit ++;
                break;
            case HitType.Great:
                totalScore += 10;
                currentCombo ++;
                greatHit ++;
                break;
            case HitType.Good:
                totalScore += 5;
                currentCombo ++;
                goodHit ++;
                break;
            case HitType.Miss:
                totalScore += 0;
                currentCombo = 0;  // Breaks the combo
                badHit ++;
                break;
        }
        
        // Dialogue that appears with the combos
        if(currentCombo > 5 && currentCombo < 10)
            dialogueManager.ShowDialogue("A decent tune. Here, have a copper");
        else if(currentCombo > 10 && currentCombo < 20)
            dialogueManager.ShowDialogue("Best song I've heard since the last age!");
        else if(currentCombo > 20)
            dialogueManager.ShowDialogue("Who is this performer? They must play for the King!");
        else if(currentCombo < 5)
           dialogueManager.ShowDialogue("Boooo!! Get off the stage!!");
            
        // Ambience that appears with combos
        if(ambienceManager != null)
        {
            ambienceManager.PlayCheer(currentCombo);
            ambienceManager.PlayBoo(currentCombo);
            Debug.Log("Playing at " + currentCombo);
        }
        // Update the score everytime its called
        UpdateScoreUI();

    }
    public void UpdateScoreUI()
    {
        if(scoreText != null)
            scoreText.text = $"Score: {totalScore} \n Combo: {currentCombo}";
        Debug.Log("Total Score: " + totalScore);
        Debug.Log("Total Combo: " +currentCombo);
        Debug.Log("Perfect: " + perfectHit);
        Debug.Log("Great: " + greatHit);
        Debug.Log("Good" + goodHit);
        Debug.Log("Miss" + badHit);
    
    }

    public void OnSongEnd()
    {
        Debug.Log("Song ended");
        Debug.Log("Number of Missed Notes " + badHit);

        if(badHit < 30) // Player passed the stage
        {
            stageSelect.stageComplete(); // Tell stage that player completed this stage
            dialogue.setCompletion(); // Tell dialogue that they passed the stage
            dialogue.setEndTrue(); // Tell dialogue that its an end scene
            SceneManager.LoadScene("Dialogue Scene"); // Load the dialogue scene with ending dialogue
        }
        else // Player sucks
        {
            // Player did not complete stage, and thus did not pass the stage.
            dialogue.setEndTrue(); // They did finish the stage though, just not with a pass
            SceneManager.LoadScene("Dialogue Scene"); // Time to get told off.
        }
    }

}
