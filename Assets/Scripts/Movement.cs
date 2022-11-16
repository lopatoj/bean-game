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
    private int orientation2 = 1;
    private float sprinting = 1f;
    private float verticalRotation = 0f;
    private float angle = 1f;
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
        //grounded = Physics.SphereCheck(Feet, .1f, Ground);
    }

    void UpCheck()
    {
        while (verticalRotation < 0f)
        {
            verticalRotation += 360f;
        }

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

        if (rotation > 0f && rotation < 180f)
        {
            orientation2 = 1;
        }
        else if (rotation > 180f && rotation < 360f)
        {
            orientation2 = -1;
        }

        //Debug.Log(orientation);
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
        velocity.y += gravity * orientation2 * angle * Time.deltaTime;

        if (grounded && orientation2 == orientation)
        {
            velocity.y = 0f;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer.Equals(Ground))
        {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer.Equals(Ground))
        {
            grounded = false;
        }
    }
}