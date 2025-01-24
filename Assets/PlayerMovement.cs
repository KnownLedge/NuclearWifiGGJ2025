using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // How fast the player moves
    public float maxSpeed = 15; // Top speed player can move at(horizontally)

    public float jumpForce = 5; //How strong the players jump is

    [Header("Debug")]
    public float inpX; //Horizontal input
    public float inpY; //Vertical input

    public bool grounded = true; //Wheter player can jump

    public Rigidbody2D rb; //Reference to rigidbody, for physics

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get reference to players rigidbody component
    }



    void getInput()
    {
        inpX = Input.GetAxis("Horizontal");
        inpY = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        getInput();

    }

     void FixedUpdate()
    {

        // HORIZONTAL MOVEMENT

        rb.AddForce(Vector2.right * inpX * moveSpeed); //Add movespeed to player as a force
        float xVelocity = rb.velocity.x; // Get horizontal velocity
        xVelocity = Mathf.Clamp(xVelocity, -maxSpeed, maxSpeed); //Limit horizontal velocity to max speed

        rb.velocity = new Vector2(xVelocity, rb.velocity.y);  // Set velocity to use our new horizontal speed (if it wasn't limited this changes nothing)
        //Probably not the best way to go about this

        //JUMPING
        if (inpY > 0.4 && grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            grounded = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 normal = collision.GetContact(0).normal;
        if(normal == Vector3.up)
        {
            grounded = true;
        }
        
    }

}
