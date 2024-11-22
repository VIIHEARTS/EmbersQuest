
using Unity.VisualScripting;
using UnityEngine;

public class WindStorm : MonoBehaviour
{

    public float windForce = 2f;
    public float stormCountdown = 3f;
    public float stormDuration = 10f;
    public float blockDuration;

    public bool isStorming = false;
    public bool isBlocked = false;

    public bool countdownStarted = false;
    public Rigidbody2D rb;

    public GameObject stormTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == stormTrigger)
        {
            countdownStarted = true;
        }

    }
    public void Update()
    {
        
        if (countdownStarted && stormCountdown > 0)
        {
            stormCountdown -= Time.deltaTime;

            if (stormCountdown <= 0)
            {
                isStorming = true;
            }
            
        }



        if (isStorming == !isBlocked)
        {
            if (stormDuration > 0 && isStorming == true)
            {
                stormDuration -= Time.deltaTime;
            }
            else
            {
                ResetStorm();
                
            }
        }

        

        if (Input.GetKeyDown(KeyCode.F) && blockDuration == 7)
        {
             isBlocked = !isBlocked;
        }

        if (isBlocked && blockDuration > 0)
        {
            blockDuration -= Time.deltaTime;
        }
        else
        {
            blockDuration = 7;
            isBlocked = false;
        }

        if (isStorming && !isBlocked)
        {
            rb.AddForce(new Vector2(-windForce, 0), ForceMode2D.Force);
        }
    }

    private void ResetStorm()
    {
        stormCountdown = 3f;
        stormDuration = 7f;
        isStorming = false;
        countdownStarted = false;
    }


}
