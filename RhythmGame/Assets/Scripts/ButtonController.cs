using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public KeyCode keyPressed;
    public KeyCode keyPressedSecondary; // For the middle key
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyPressed) || Input.GetKeyDown(keyPressedSecondary))
        {
            spriteRenderer.color = new Color(1, 1, 1, 1f);
        }
        if(Input.GetKeyUp(keyPressed) || Input.GetKeyUp(keyPressedSecondary))
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.10f);
        }
    }
}
