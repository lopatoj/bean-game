using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Movement : MonoBehaviour
{
    // Objects from game scene that need to be referenced by this class
    private CharacterController Player;
    public Transform Camera;
    public Transform Center;
    public LayerMask Ground;
    public LayerMask User;

    // Multiplier values initialized in Unity script menu
    public float walkSpeed;
    public float gravityAcceleration;
    public float jumpSpeed;
    public float mouseSensitivity;

    // Velocity applied to player each frame
    private Vector3 velocity;
    private Vector3 previousVelocity;

    // Rotation value about x axis of camera
    private float verticalRotation = 0f;

    // Private values that change every frame based on rotation & collisions
    private int playerOrientation = 1;
    private int gravityDirection = 1;
    private bool grounded = false;
    private float pastX = 0f;
    
    // Constant multiplier that matches mouse sensitivity values to other similar games
    private const float standardMultiplier = 26.33405852f;

    // Runs before first frame
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
        //Debug.Log("grounded: " + (grounded ? "Yes" : "No"));

        // Tests for orientation of Camera and whether Player is standing on Ground
        OrientationCheck();
        CollisionCheck();
        BoundsCheck();

        // Applies translation and rotation to Player and Camera based on input
        Looking();
        Walking();
        Falling();
        Jumping();

        // Applies velocity to player multiplied by time passed since last frame
        Player.Move(velocity * Time.deltaTime);
    }

    void OrientationCheck()
    {
        // While loops make sure verticalRotation is between 0 and 360
        while (verticalRotation < 0f)
        {
            verticalRotation += 360f;
        }

        while (verticalRotation > 360f)
        {
            verticalRotation -= 360f;
        }

        //Debug.Log(verticalRotation % 360);

        // If Camera is facing forward relative to Player, then orientation = 1, else orientation = -1
        if (verticalRotation < 90f ^ verticalRotation > 270f)
        {
            playerOrientation = 1;
        }
        else if (verticalRotation > 90f && verticalRotation < 270f)
        {
            playerOrientation = -1;
        }

        // If Camera is facing up relative to Player, then orientation = 1, else orientation = -1
        if (verticalRotation > 180f && verticalRotation < 360f)
        {
            gravityDirection = 1;
        }
        else if (verticalRotation > 0f && verticalRotation < 180f)
        {
            gravityDirection = -1;
        }
    }

    void BoundsCheck()
    {
        if(!Physics.CheckSphere(Center.position, 3f, User))
        {
            transform.Translate(Center.position - transform.position);
        }
    }

    // If any object of the layer Ground is present beneath Player, then Player is standing on ground and therefore grounded = true, else grounded = false
    void CollisionCheck()
    {
        grounded = Physics.CheckSphere(transform.position - transform.up * playerOrientation, .1f, Ground);
    }

    // Rotates camera vertically (about x axis) based on mouse Y movement and rotates player horizontally (about y axis) based on mouse X movement
    void Looking()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity * standardMultiplier * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity * standardMultiplier * Time.deltaTime;

        verticalRotation -= y;

        Camera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * x * playerOrientation);
    }

    // Translates player horizontally if W-A-S-D keys are pressed
    void Walking()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical") * playerOrientation;

        Vector3 move = transform.forward * v + transform.right * h;

        velocity.x = move.x * walkSpeed;
        velocity.z = move.z * walkSpeed;

        pastX = v;
    }

    // If Player is standing on Ground and space key is pressed, then Y velocity is set to the initial jump speed * orientation of Player
    void Jumping()
    {
        if (grounded && Input.GetButton("Jump"))
        {
            velocity.y = jumpSpeed * playerOrientation;
        }
    }

    // If Player is not standing on Ground, then apply gravitational acceleration relative to orientation of player
    void Falling()
    {
        if (grounded && gravityDirection != playerOrientation)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y -= gravityAcceleration * playerOrientation * Time.deltaTime;

            // Air Resistence
            velocity.y -= .4f * velocity.y * Time.deltaTime;
        }
    }
}