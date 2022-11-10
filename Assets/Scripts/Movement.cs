using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform Eyes;

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

    bool Grounded()
    {
        return true;
    }
}
