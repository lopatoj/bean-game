using UnityEngine;

public class Movement : MonoBehaviour
{    
    // Private values that change every frame based on rotation & collisions
    private int _playerOrientation;
    private int _gravityDirection;
    private bool _grounded;

    // Velocity applied to player each frame
    private Vector3 _velocity;

    // Rotation value about x axis of camera
    private float _verticalRotation;

    // Objects from game scene that need to be referenced by this class
    [SerializeField] private Transform Camera;

    [SerializeField] private Global.Global Global;
    
    [SerializeField] private LayerMask Ground;
    
    [SerializeField] private CharacterController Player;

    [SerializeField] private LayerMask User;

    // Coefficient values initialized in Unity script menu
    public float walkSpeed;
    public float gravityAcceleration;
    public float jumpSpeed;
    
    // Constant multiplier that matches mouse sensitivity values to other similar games
    private const float StandardMultiplier = 60f;

    // Runs once before first frame
    private void Start()
    {
        // Assigns CharacterController component of the object this script is assigned to to variable Player
        Player = GetComponent<CharacterController>();

        // Sets intitial instance variables
        _verticalRotation = 0f;
        _playerOrientation = 1;
        _gravityDirection = 1;
        _grounded = false;
    }

    // Runs every frame
    private void Update()
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
        Player.Move(_velocity * Time.deltaTime);
    }

    private void OrientationCheck()
    {
        // While loops make sure verticalRotation is between 0 and 360
        while (_verticalRotation < 0f) _verticalRotation += 360f;

        while (_verticalRotation > 360f) _verticalRotation -= 360f;

        //Debug.Log(verticalRotation % 360);

        // If Camera is facing forward relative to Player, then orientation = 1, else orientation = -1
        if ((_verticalRotation < 90f) ^ (_verticalRotation > 270f))
            _playerOrientation = 1;
        else if (_verticalRotation > 90f && _verticalRotation < 270f) _playerOrientation = -1;

        // If Camera is facing up relative to Player, then orientation = 1, else orientation = -1
        if (_verticalRotation > 180f && _verticalRotation < 360f)
            _gravityDirection = 1;
        else if (_verticalRotation > 0f && _verticalRotation < 180f) _gravityDirection = -1;
    }

    // If player is more than 300
    private void BoundsCheck()
    {
        // Return if player is within sphere
        if (Physics.CheckSphere(Vector3.zero, 300f, User)) return;

        // Move player to center of circle if outside of circle
        transform.Translate(Vector3.zero - transform.position);
    }

    // If any object of the layer Ground is present beneath Player, then Player is standing on ground and therefore grounded = true, else grounded = false
    private void CollisionCheck()
    {
        var t = transform;

        // Check if ground exists at player feet
        _grounded = Physics.CheckSphere(t.position - t.up * _playerOrientation, .1f, Ground);
    }

    // Rotates camera vertically (about x axis) based on mouse Y movement and rotates player horizontally (about y axis) based on mouse X movement
    private void Looking()
    {
        // Get input values from mouse
        var x = Input.GetAxis("Mouse X") * Global.sensitivity * StandardMultiplier * Time.deltaTime;
        var y = Input.GetAxis("Mouse Y") * Global.sensitivity * StandardMultiplier * Time.deltaTime;

        // Set change vertical rotation based on mouse y movement
        _verticalRotation -= y;

        // Rotate camera by y rotation
        Camera.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);

        // Rotate player horizontally
        transform.Rotate(Vector3.up * (_playerOrientation * x));
    }

    // Translates player horizontally if W-A-S-D keys are pressed
    private void Walking()
    {
        // Get input values from keyboard
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical") * _playerOrientation;

        // Creates movement vector
        var t = transform;
        var move = t.forward * v + t.right * h;

        // Sets horizontal velocity to movement vector
        _velocity.x = move.x * walkSpeed;
        _velocity.z = move.z * walkSpeed;
    }

    // If Player is standing on Ground and space key is pressed, then Y velocity is set to the initial jump speed * orientation of Player
    private void Jumping()
    {
        if (_grounded && Input.GetButton("Jump")) _velocity.y = jumpSpeed * _playerOrientation;
    }

    // If Player is not standing on Ground, then apply gravitational acceleration relative to orientation of player
    private void Falling()
    {
        if (_grounded && _gravityDirection != _playerOrientation)
        {
            _velocity.y = 0f;
        }
        else
        {
            _velocity.y -= gravityAcceleration * _playerOrientation * Time.deltaTime;

            // Air Resistance
            _velocity.y -= .4f * _velocity.y * Time.deltaTime;
        }
    }

    // Return velocity vector
    public Vector3 GetVelocity()
    {
        return _velocity;
    }
}