using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HitType
{
    None,
    Early,
    Perfect,
    Late
}
public class NoteController : MonoBehaviour
{
    // Update is called once per frame
    public InputController inputController;
    // public HitZone hitZone;
    public Transform hitLine;
    private SpriteRenderer spriteRenderer;
    
    // private bool isInHitZone = false;
    public float moveSpeed = 3f;
    public float perfectWindow = 0.2f;
    public float goodWindow = 0.6f;
    public float badWindow = 0.8f;

    private bool isStopped = false;
    private HitType currentHitType = HitType.None;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
         spriteRenderer.color =  new Color(0, 0, 1); 
    }

    void Update()
    {
        if (!isStopped)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }

        // Only allow hit if inside hit zone
        if (inputController.getKeyDown() && !isStopped)
        {
            CheckHitStatus();
        }
    }
    public void CheckHitStatus()
    {
        float distance = Mathf.Abs(transform.position.y - hitLine.position.y);

        if (distance <= perfectWindow)
        {
            Debug.Log("PERFECT");
            HitNote(Color.green);
        }
        else if (distance <= goodWindow)
        {
            Debug.Log("GOOD");
            HitNote(Color.yellow);
        }
        else if (distance <= badWindow)
        {
            Debug.Log("LATE / EARLY");
            HitNote(Color.red);
        }
        else
        {
            Debug.Log("MISS");
        }
    }
 
    void HitNote(Color hitColor)
    {
        isStopped = true;
        spriteRenderer.color =  hitColor; 

        StartCoroutine(DisableNote());
    }

    IEnumerator DisableNote()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Perfect"))
    //     {
    //         isInHitZone = true;
    //         currentHitType = HitType.Perfect;
    //     }
    //     if (other.CompareTag("Early"))
    //     {
    //         isInHitZone = true;
    //         currentHitType = HitType.Early;
    //     }
    //     if (other.CompareTag("Late"))
    //     {
    //         isInHitZone = true;
    //         currentHitType = HitType.Late;
    //         Debug.Log("InLateZone");
    //     }
    // }
    

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Perfect") || other.CompareTag("Early") || other.CompareTag("Late"))
    //     {
    //         isInHitZone = false;
    //     }
    // }


}
