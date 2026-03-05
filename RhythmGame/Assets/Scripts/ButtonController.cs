using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public KeyCode keyPressed;
    public KeyCode keyPressedSecondary = KeyCode.None;

    public float moveSpeed = 3f;

    private bool isStopped = false;
    private bool isInHitZone = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.blue;
    }

    void Update()
    {
        if (!isStopped)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }

        bool keyDown = Input.GetKeyDown(keyPressed) ||
                       (keyPressedSecondary != KeyCode.None && Input.GetKeyDown(keyPressedSecondary));

        // Only allow hit if inside hit zone
        if (keyDown && isInHitZone && !isStopped)
        {
            HitNote();
        }
    }

    void HitNote()
    {
        isStopped = true;
        meshRenderer.material.color = Color.green;

        Destroy(gameObject, 0.3f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitZone"))
        {
            isInHitZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HitZone"))
        {
            isInHitZone = false;
        }
    }
}