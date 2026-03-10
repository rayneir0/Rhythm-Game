using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculateScore : MonoBehaviour
{
    public int totalScore;
    public int currentCombo;
    public TextMeshProUGUI scoreText;
    public DialogueManager dialogueManager;
    public AmbienceManager ambienceManager;
    public void AddScore(NoteController note)
    {
        switch (note.currentHitType)
        {   // Checks the current hit type of note, and gives it a score
            case HitType.Perfect:
                totalScore  += 20;
                currentCombo ++; // Adds to the combo
                break;
            case HitType.Great:
                totalScore += 10;
                currentCombo ++;
                break;
            case HitType.Good:
                totalScore += 5;
                currentCombo ++;
                break;
            case HitType.Miss:
                totalScore += 0;
                currentCombo = 0;  // Breaks the combo
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
            scoreText.text = $"Score: {totalScore} | Combo: {currentCombo}";
        
   
    }
}
