using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float size = 1.0f;
   public  float speed = 0.2f;
    public float directionY = 0.5f;

    public GameController gameRef; // Reference to gamecontroller script for events like bubble popping

    public Vector2 spawnPoint; //Where the bubble spawns


    public Rigidbody2D rb; //Reference to Rigidbody

    private void Start()
    {
        spawnPoint = transform.position;
    }


    void FixedUpdate()
    {
        Vector3 scale = new Vector3();
        scale.x = size;
        scale.y = size;
        transform.localScale = scale;

        Vector3 position = transform.localPosition;
        position.y += speed * directionY;
        transform.localPosition = position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.name)
        {
            case "Wall":
                directionY = -directionY;
                break;

            case "Ground":
         //       transform.position;
                break;

        }

        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Rigidbody2D>() != null) {
                Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();
                rb.linearVelocity = new Vector2(otherRB.linearVelocityX, -directionY);
            }

        }



    }
}
