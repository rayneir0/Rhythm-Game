using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private Coroutine hideCoroutine;

    // Flexible function to show the dialogue
    public void ShowDialogue(string text, float duration = 2f)
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        dialogueText.text = text;
        dialogueText.gameObject.SetActive(true);

        hideCoroutine = StartCoroutine(HideAfterTime(duration));
    }

    private IEnumerator HideAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueText.gameObject.SetActive(false);
    }
}

