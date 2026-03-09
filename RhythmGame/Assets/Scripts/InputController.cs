using UnityEngine;

public class InputController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public KeyCode keyPressed;
    public KeyCode keyPressedSecondary = KeyCode.None;
    private bool keyDown = false;
    private bool keyUp = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0.25f); 
    }

    void Update()
    {
        keyDown = Input.GetKeyDown(keyPressed) ||
                       (keyPressedSecondary != KeyCode.None && Input.GetKeyDown(keyPressedSecondary));
        
        keyUp = Input.GetKeyUp(keyPressed) ||
                       (keyPressedSecondary != KeyCode.None && Input.GetKeyUp(keyPressedSecondary));
        // For visual interaction
        if(keyDown)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1f);
        }

        if(keyUp)
        {
            spriteRenderer.color =  new Color(1, 1, 1, 0.25f); 
        }

    }

    public bool getKeyDown()
    {
        return keyDown;
    }
     public bool getKeyUp()
    {
        return keyUp;
    }



}