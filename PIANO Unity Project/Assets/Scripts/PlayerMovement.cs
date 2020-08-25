using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // put this on the player with a player controller and a empty Object that functions as a GroundCheck.
    // Add a layer for Ground and change all Groundobjects to that Layer. Then switch the groundMask to Ground.
    //uncomment below to enable jumping

    public CharacterController controller; // reference the player controller


    public float speed = 12f;
    public float gravity = -9.81f;
    //public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.04f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //Jumping
        /* if(Input.GetButtonDown("Jump") && isGrounded
          {
               velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
          }
        */


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
