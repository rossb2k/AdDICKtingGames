using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    public float jump = 600f;
    public float moveSpeed = 5f;
    [SerializeField] private float Move;
    private Vector2 movement;
    // private bool isFacingRight = true;

    public bool canJump = true;

    //powerups and related junk

    private float originalSpeed;
    private Color originalColor;

    private float powerUpTimer = 0.0f; // Timer to track the duration of power-up effects
    private bool hasPowerUp = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalSpeed = moveSpeed;
        originalColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {

        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(Move * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && canJump){
            rb.AddForce(new Vector2(rb.velocity.x, jump));
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

    //movement
    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.x > 0 ){
            animator.SetFloat("X", movement.x);
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector2(1f,1f);
            
        } else if (movement.x < 0){
            animator.SetFloat("X", movement.x);
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector2(-1f,1f);

        } else{
            animator.SetBool("isWalking", false);
            }
    }



    //ground check
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            Debug.Log("Grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
            Debug.Log("Not Grounded");
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