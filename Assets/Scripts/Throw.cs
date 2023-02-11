using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField]
    private GameObject Bean;
    [SerializeField]
    private Transform Hand;
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private float force;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float lifetime;

    private float throwTimer;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetAxis("Fire1") == 1 && throwTimer > cooldown) 
        {
            Launch();
            throwTimer = 0f;
        }

        throwTimer += Time.deltaTime;
    }

    private void Launch() 
    {
        GameObject b = Instantiate(Bean, Hand.position, Random.rotation);

        b.GetComponent<Rigidbody>().AddForce(transform.forward * force + Player.GetComponent<CharacterController>().velocity, ForceMode.VelocityChange);
        b.GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles);

        Destroy(b, lifetime);
    }
}