using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform Eyes;
    public Transform Feet;
    public LayerMask Ground;

    public float speed;
    public float gravity;

    private Vector3 velocity;
    private int orientation = 1;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Walk();

        GetComponent<CharacterController>().Move(velocity);
    }

    void Walk()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(Grounded())
        {
            velocity.x = h * speed * Time.deltaTime * orientation;
            velocity.y = h * speed * Time.deltaTime * orientation;
        }
    }

    void Look()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");


    }

    bool Grounded()
    {
        return Physics.SphereCast(Feet.position, .03f, Vector3.up, out RaycastHit a, .03f, Ground);
    }
}
