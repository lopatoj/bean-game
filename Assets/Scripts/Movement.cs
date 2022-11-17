using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Movement : MonoBehaviour
{
    // Constant multiplier that matches mouse sensitivity values to other similar games
    private const float standardMultiplier = 26.33405852f;

    // Objects from game scene that need to be referenced by this class
    private CharacterController Player;
    public Transform Camera;
    public LayerMask Ground;

    // Multiplier values initialized in Unity script menu
    public float walkSpeed;
    public float gravityAcceleration;
    public float jumpSpeed;
    public float mouseSensitivity;

    // Velocity applied to player each frame
    private Vector3 velocity;

    // Rotation value about x axis of camera
    private float verticalRotation = 0f;

    // Private values that change every frame based on rotation & collisions
    private int playerOrientation = 1;
    private int gravityDirection = 1;
    bool grounded = false;

    // Runs at start of game
    void Start()
    {
        // Keeps mouse cursor in center of screen & hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Assigns CharacterController component of the object this script is assigned to to variable Player
        Player = GetComponent<CharacterController>();
    }

    // Runs every frame
    void Update()
    {
        //Debug.Log("playerOrientation: " + (playerOrientation == 1 ? "Up" : "Down"));
        //Debug.Log("gravityDirection: " + (gravityDirection == 1 ? "Up" : "Down"));
        Debug.Log("grounded: " + (grounded ? "Yes" : "No"));

        // 
        CollisionCheck();
        OrientationCheck();

        Looking();
        Walking();
        Falling();
        Jumping();

        // Applies velocity to player multiplied by time passed since last frame()
        Player.Move(velocity * Time.deltaTime);
    }

    void CollisionCheck()
    {
        grounded = Physics.CheckCapsule(transform.position + transform.up * .6f, transform.position - transform.up * .6f, .49f, Ground);
    }

    void OrientationCheck()
    {
        while (verticalRotation < 0f)
        {
            verticalRotation += 360f;
        }

        //Debug.Log(verticalRotation % 360);

        float rotation = verticalRotation % 360;

        if (rotation > 90f && rotation < 270f)
        {
            playerOrientation = -1;
        } 
        else if(rotation < 90f ^ rotation > 270f)
        {
            playerOrientation = 1;
        }

        if (rotation > 0f && rotation < 180f)
        {
            gravityDirection = 1;
        }
        else if (rotation > 180f && rotation < 360f)
        {
            gravityDirection = -1;
        }
    }

    void Looking()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity * standardMultiplier * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity * standardMultiplier * Time.deltaTime;

        verticalRotation -= y;

        Camera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * x * playerOrientation);
    }

    void Walking()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical") * playerOrientation;

        Vector3 move = transform.forward * v + transform.right * h;

        velocity.x = move.x * walkSpeed;
        velocity.z = move.z * walkSpeed;
    }

    void Jumping()
    {
        if (grounded && Input.GetButton("Jump"))
        {
            velocity.y = jumpSpeed * playerOrientation;
        }
    }

    void Falling()
    {
        if (grounded && gravityDirection == playerOrientation)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y -= gravityAcceleration * gravityDirection * Time.deltaTime;
        }
    }
}