using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum HitType
{
    None,
    Perfect,
    Great,
    Good,
    Miss
}
public class NoteController : MonoBehaviour
{
    // Update is called once per frame
    [HideInInspector]public InputController inputController;
    // public HitZone hitZone;
    public Transform hitLine;
    public HitType currentHitType = HitType.None;
    public CalculateScore score;
    public TextMeshProUGUI feedbackText;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 3f;
    public float perfectWindow = 0.2f;
    public float goodWindow = 0.6f;
    public float badWindow = 0.8f;
    public bool isHit = false;
    private bool isStopped = false;
    private Coroutine hideCoroutine;

    [SerializeField] private ParticleSystem hitEffect;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }
    void OnEnable()
    {
        isStopped = false;
        spriteRenderer.color = Color.white;
        isHit = false;
    }
    void Start()
    {
        spriteRenderer.color =  new Color(1, 1, 1); // white colour for the note
    }

    void Update()
    {
        if (!isStopped)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }

        if (transform.position.y < hitLine.position.y - 2f && !isHit) // If the note hasn't been hit and it has been missed
        {
            currentHitType = HitType.Miss;
            HitNote(Color.gray, "Miss");  // reuse the same HitNote method
            isHit = true; // mark this note as already processed
        }
        if(transform.position.y < hitLine.position.y - 6f)
        {
            currentHitType = HitType.Miss;
            HitNote(Color.gray, "Miss");
            isHit = true;
            gameObject.SetActive(false);
        }
    }
    public void CheckHitStatus()
    {
        if (isHit) return;
        isHit = true;
        if (hitLine == null)
        {
            Debug.LogWarning("HitLine not assigned for note!");
            return;
        }

        float distance = Mathf.Abs(transform.position.y - hitLine.position.y); // Calculates the distance from hitline

        if (distance <= perfectWindow)
        {
            Debug.Log("PERFECT");
            currentHitType = HitType.Perfect;
            HitNote(Color.green, "Perfect"); // Show colour and text feedback
        }
        else if (distance <= goodWindow)
        {
            Debug.Log("GREAT");
            currentHitType = HitType.Great;
            HitNote(Color.yellow, "Great");
        }
        else if (distance <= badWindow)
        {
            Debug.Log("GOOD");
            currentHitType = HitType.Good;
            HitNote(Color.red, "Good");
        }
        else
        {
            Debug.Log("MISS");
            currentHitType = HitType.Miss;
        }
     
    }
 
    void HitNote(Color hitColor, string feedback)
    {
        isStopped = true;
        spriteRenderer.color =  hitColor; // Give the sprite the colour
        score.AddScore(this); // Add the score based on Hit Type

        if (feedback != "Miss")
        {
            ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }

        if (feedbackText != null)
        {
            feedbackText.text = feedback; // Set the text
            feedbackText.gameObject.SetActive(true);

            // Hide after 0.3s
            if(hideCoroutine != null)
                StopCoroutine(hideCoroutine);
            hideCoroutine = StartCoroutine(HideFeedback(0.5f));

        }

        StartCoroutine(DisableNote());
    }
    private IEnumerator HideFeedback(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }

    IEnumerator DisableNote()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }

  

}
