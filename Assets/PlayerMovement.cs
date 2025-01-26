using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // How fast the player moves
    public float maxSpeed = 15; // Top speed player can move at(horizontally)

    public float jumpForce = 5; //How strong the players jump is

    [Header("Input Axis's")]
    public string horizontalAxis = "Horizontal";
    public string jumpAxis = "Jump";
    public string verticalAxis = "Vertical";


    [Header("Debug")]
    public float inpX; //Horizontal input
    public float inpY; //Vertical input

    public bool grounded = true; //Wheter player can jump

    public Rigidbody2D rb; //Reference to rigidbody, for physics

    public PlayerInput playInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get reference to players rigidbody component
        GameObject gameControl = GameObject.Find("Main Camera");
        GameController gameManage = gameControl.GetComponent<GameController>();
        gameManage.ReceivePlayer(gameObject);
       // playInput.

    }



    void getInput()
    {
       // inpX = Input.GetAxis(horizontalAxis);
        inpY = Input.GetAxis(jumpAxis); //Getting vertical from jump to overwrite pressing up to jump, to being pressing the jump button to jump
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
        float xVelocity = rb.linearVelocity.x; // Get horizontal velocity
        xVelocity = Mathf.Clamp(xVelocity, -maxSpeed, maxSpeed); //Limit horizontal velocity to max speed

        rb.linearVelocity = new Vector2(xVelocity, rb.linearVelocity.y);  // Set velocity to use our new horizontal speed (if it wasn't limited this changes nothing)
        //Probably not the best way to go about this

        //JUMPING
        //if (inpY > 0.4 && grounded)
        //{
        //    rb.AddForce(Vector2.up * jumpForce);
        //    grounded = false;
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 normal = collision.GetContact(0).normal;
        if(normal == Vector3.up)
        {
            grounded = true;
        }
        
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("ITS JUMPING?");

        if (grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            grounded = false;
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("ITS MOVING?");

        Vector2 moveVector = context.ReadValue<Vector2>();
        inpX = moveVector.x;
    }


}
