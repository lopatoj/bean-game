using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController Player;
    public Transform Eyes;
    public Transform Feet;
    public LayerMask Ground;

    public float speed;
    public float jump;
    public float gravity;
    public float sensitivity;

    private Vector3 velocity;
    private int orientation = 1;
    private float xRot = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Player = GetComponent<CharacterController>();
    }

    void Update()
    {
        Walk();
        Look();
        Jump();

        Player.Move(velocity);

        Fall();

        Debug.Log(Grounded());
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

    void Jump()
    {
        bool s = Input.GetButton("Jump");

        if(Grounded() && s)
        {
            velocity.y = jump;
        }
    }

    void Fall()
    {
        velocity.y += gravity * Time.deltaTime;

        if(Grounded())
        {
            velocity.y = 0f;
        }
    }

    void Look()
    {
        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= y;
        xRot = Mathf.Clamp(xRot, -90, 90);

        transform.Rotate(0f, x, 0);
        Eyes.transform.localEulerAngles = new Vector3(xRot, 0f, 0f);
    }

    bool Grounded()
    {
        return Physics.SphereCast(Feet.position, 1f, Vector3.forward, out RaycastHit hit, 1f, Ground);
    }
}
