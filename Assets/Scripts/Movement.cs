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
        Looking();
        Walking();
        Falling();
        Jumping();

        Player.Move(velocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        grounded = Physics.CheckSphere(Feet.position, .2f, Ground);
    }

    void UpCheck()
    {

    }

    void Looking()
    {
        float x = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        float y = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        verticalRotation -= y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        Eyes.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * x);
    }

    void Walking()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * v + transform.right * h;

        float temp = velocity.y;
        velocity += 5f * move * speed * orientation * Time.deltaTime * (grounded ? 1f : 0.3f);
        velocity.y = temp;

        if(grounded)
        {

        }
    }

    void Jumping()
    {
        if (grounded && Input.GetButton("Jump"))
        {
            velocity.y = jump;
        }
    }

    void Falling()
    {
        velocity.y += gravity * Time.deltaTime;

        if (grounded)
        {
            velocity.y = 0f;
        }
    }
}