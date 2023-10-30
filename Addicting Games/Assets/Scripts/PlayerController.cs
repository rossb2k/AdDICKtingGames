using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    private Rigidbody2D rb;
    private bool canJump = true;
    private float originalSpeed; // Store the original speed
    private Color originalColor; // Store the original color

    private float powerUpTimer = 0.0f; // Timer to track the duration of power-up effects
    private bool hasPowerUp = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        originalSpeed = moveSpeed; // Store the original speed on Start
        originalColor = GetComponent<Renderer>().material.color; // Store the original color on Start
    }

    void Update()
    {
        // Player movement
        float moveDirection = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = rb.velocity;
        moveVelocity.x = moveDirection * moveSpeed;
        rb.velocity = moveVelocity;

        // Player jump
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false; // Disable jumping until the player lands again
        }

        if (hasPowerUp)
        {
            // If the power-up timer exceeds the desired duration, revert the effects
            if (Time.time > powerUpTimer)
            {
                RevertPowerUpEffects();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Re-enable jumping when the player lands on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            // Set the power-up timer and mark that the player has a power-up
            hasPowerUp = true;
            powerUpTimer = Time.time + 5.0f; // 5 seconds duration

            // Collect the power-up object (destroy it)
            Destroy(other.gameObject);
        }
    }

    void RevertPowerUpEffects()
    {
        // Revert the power-up effects on the player character
        hasPowerUp = false;
        moveSpeed = originalSpeed;
        ChangeColor(originalColor);
    }



    public void ApplySpeedIncrease(float speedIncrease)
    {
        // Apply the speed increase
        moveSpeed += speedIncrease;
    }

    public void RevertSpeedIncrease()
    {
        // Revert the speed increase to the original speed
        moveSpeed = originalSpeed;
    }

    public void ChangeColor(Color newColor)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = newColor;
        }
    }
    public void RevertColorChange()
    {
        // Revert the player's color to the original color
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = originalColor;
        }
    }
}

