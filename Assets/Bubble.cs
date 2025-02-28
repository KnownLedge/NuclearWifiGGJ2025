using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float size = 1.0f;
   public  float speed = 0.2f;
    public float directionY = 0.5f;

    public GameController gameRef; // Reference to gamecontroller script for events like bubble popping

    public Vector2 spawnPoint; //Where the bubble spawns

    public SpriteRenderer spriteRenderer; //Reference to sprite component, we can use this to change the sprite in the code

    public Rigidbody2D rb; //Reference to Rigidbody

    public Color[] colors = new Color[3]; //Colors for the bubble to swap to

    private void Start()
    {
        spawnPoint = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                rb.linearVelocity = new Vector2(-rb.linearVelocityX * 2, rb.linearVelocityY);
                break;

            case "Roof":
                directionY = -directionY;
                break;

            case "Ground":
                transform.position = spawnPoint;
                rb.linearVelocity = new Vector2(0, 0);
                gameRef.BubbleHitFloor();

                break;

        }

        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Rigidbody2D>() != null) {
                Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();
                rb.linearVelocity = new Vector2(otherRB.linearVelocityX, 0);
            }

        }


    }

    public void SwitchBubble(int turn)
    {

                spriteRenderer.color = colors[turn];



    }


}
