using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Throw : MonoBehaviour
{
    [SerializeField] private GameObject Bean;

    [SerializeField] private float cooldown;

    [SerializeField] private float force;

    [SerializeField] private Transform Hand;

    [SerializeField] private float lifetime;

    [SerializeField] private GameObject Player;

    private float _throwTimer;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetAxis("Fire1") >= 1 && _throwTimer > cooldown)
        {
            Action launch = Launch;
            
            launch();
            
            _throwTimer = 0f;
        }

        _throwTimer += Time.deltaTime;
    }

    private void Launch()
    {
        var b = Instantiate(Bean, Hand.position, Random.rotation);

        b.GetComponent<Rigidbody>()
            .AddForce(transform.forward * force + Player.GetComponent<CharacterController>().velocity,
                ForceMode.VelocityChange);
        b.GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles);

        Destroy(b, lifetime);
    }
}