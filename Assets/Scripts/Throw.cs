using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Throw : MonoBehaviour
{
    // Objects from game scene that need to be referenced by this class
    [SerializeField] private GameObject Bean;

    [SerializeField] private float cooldown;

    [SerializeField] private float force;

    [SerializeField] private Transform Hand;

    [SerializeField] private Transform Direction;

    [SerializeField] private float lifetime;

    [SerializeField] private GameObject Player;

    // Private timer value that increases with time
    private float _throwTimer;

    // Runs once before first frame
    private void Start()
    {
        _throwTimer = 0f;
    }

    // Runs once every frame
    private void Update()
    {
        // If left click pressed and throw timer is above the throwing cooldown
        if (Input.GetAxis("Fire1") >= 1 && _throwTimer > cooldown)
        {
            // Launch the bean
            Action launch = Launch;
            
            launch();
            
            // Rest throw timer
            _throwTimer = 0f;
        }

        // Increase timer by amount of time since last frame
        _throwTimer += Time.deltaTime;
    }

    // Throws the bean
    private void Launch()
    {
        // Creates the bean
        var b = Instantiate(Bean, Hand.position, Random.rotation);

        // Propels the bean
        b.GetComponent<Rigidbody>()
            .AddForce(Direction.forward * force + Player.GetComponent<CharacterController>().velocity,
                ForceMode.VelocityChange);
        
        // Spins the bean
        b.GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles);

        // Ends the bean
        Destroy(b, lifetime);
    }
}