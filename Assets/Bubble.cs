using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float size = 1.0f;
    float speed = 0.2f;
    float directionY = 0.5f;

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
        }
    }
}
