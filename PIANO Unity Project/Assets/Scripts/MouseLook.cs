using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //put this on the Camera of the Player and reference the Player Transform. 

    public float mouseSensitivity = 100f; 
    public Transform playerBody; // the player

    float xRotation = 0f; // sets the players rotation based on the movement of the mouse on the y axis
    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // minus because it would be flipped otherwise. 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // clamps the rotation, so the player can't look too much over or under his head.

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // defines the player rotation with euler angles
        playerBody.Rotate(Vector3.up * mouseX); // horizontal rotation of the player

    }
}
