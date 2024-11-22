using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    public float detectionDistance = 10f;
    public float fallSpeed = 20f;
    public Rigidbody2D rb;
    private bool playerDetected = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down * detectionDistance, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectionDistance);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Player has been Detected");
            playerDetected = true;            
            rb.bodyType = RigidbodyType2D.Dynamic;            
        }

        if (playerDetected)
        {
            rb.linearVelocity = Vector2.down * fallSpeed;
        }
    }
}
