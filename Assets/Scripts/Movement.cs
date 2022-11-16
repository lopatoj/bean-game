using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Movement : MonoBehaviour
{
    private CharacterController Player;
    public Transform Eyes;
    public Transform Feet;
    public LayerMask Ground;

    public float speed;
    public float gravity;
    public float jump;
    public float sensitivity;

    private Vector3 velocity;
    private int orientation = 1;
    private float sprinting = 1f;
    private float verticalRotation = 0f;
    bool grounded = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Player = GetComponent<CharacterController>();
    }

    void Update()
    {
        GroundCheck();
        UpCheck();
        SprintCheck();
        Looking();
        Walking();
        Falling();
        Jumping();

        Player.Move(velocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        Vector3 feetPosition = new Vector3(Feet.position.x, Feet.position.y * orientation, Feet.position.z);

        grounded = Physics.CheckSphere(feetPosition, .1f, Ground);
    }

    void UpCheck()
    {
        Debug.Log(verticalRotation % 360);

        float rotation = verticalRotation % 360;

        if (rotation > 90f && rotation < 270f)
        {
            orientation = -1;
        } 
        else if(rotation < 90f ^ rotation > 270f)
        {
            orientation = 1;
        } 
        else
        {
            orientation = 1;
        }

        Debug.Log(orientation);
    }

    void SprintCheck()
    {
        sprinting = Input.GetButton("Shift") ? 1.3f : 1f;
    }

    void Looking()
    {
        float x = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        float y = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        verticalRotation -= y;

        Eyes.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * x * orientation);
    }

    void Walking()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical") * orientation;

        Vector3 move = transform.forward * v + transform.right * h;

        float temp = velocity.y;
        velocity = move * speed * sprinting;
        velocity.y = temp;
    }

    void Jumping()
    {
        if (grounded && Input.GetButton("Jump"))
        {
            velocity.y = jump * orientation;
        }
    }

    void Falling()
    {
        velocity.y += gravity * orientation * Time.deltaTime;

        if (grounded)
        {
            velocity.y = 0f;
        }
    }
}