using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody2D rb;
    private Vector2 screenBounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if (transform.position.x > screenBounds.x)
        {
            transform.position = new Vector2(-screenBounds.x, transform.position.y);
        }
    }
}

