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
    public float gravity;
    public float sensitivity;

    private Vector3 velocity;
    private int orientation = 1;

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

        Player.Move(velocity);
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
        float s = Input.GetAxis("");
    }

    void Look()
    {
        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        float xRot = 0;
        xRot += y;
        xRot = Mathf.Clamp(xRot, -90, 90);

        transform.Rotate(0, y, 0);
        Eyes.Rotate(xRot, x, 0);
    }

    bool Grounded()
    {
        return Physics.SphereCast(Feet.position, .03f, Vector3.up, out RaycastHit a, .03f, Ground);
    }
}
