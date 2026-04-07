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
        spriteRenderer.color = new Color(0, 0, 0, 0.25f);  // White
    }

    void Update()
    {   
        // Handles the middle key which has two inputs as well
        keyDown = Input.GetKeyDown(keyPressed) ||
                       (keyPressedSecondary != KeyCode.None && Input.GetKeyDown(keyPressedSecondary));
        
        keyUp = Input.GetKeyUp(keyPressed) ||
                       (keyPressedSecondary != KeyCode.None && Input.GetKeyUp(keyPressedSecondary));
        // For visual interaction
        if(keyDown)
        {
            spriteRenderer.color = new Color(0, 0, 0, 0.45f); // White
        }

        if(keyUp)
        {
            spriteRenderer.color =  new Color(0, 0, 0, 0.25f);  // Transparent White
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