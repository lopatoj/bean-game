using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    // Objects from game scene that need to be referenced by this class
    [SerializeField] private float Speed;
    
    [SerializeField] private LayerMask Ground;
    
    [SerializeField] private LayerMask Player;
    
    [SerializeField] private LayerMask Bean;

    [SerializeField] private AudioClip Thud;
    
    [SerializeField] private AudioClip Hit;
    
    [SerializeField] private AudioClip Pop;

    // Player object in scene
    private GameObject _player;

    // Time instance variables
    private int _thisTime;
    private int _lastTime;

    // Public variables
    public int health;
    public EnemyHandler Handler;

    // Start is called before the first frame update
    void Start()
    {
        // Finds player object in scene
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Sets this time to time at start of frame
        _thisTime = (int)Math.Floor(Time.time);
        
        // If time int of last frame does not equal time int of last frame (a second has passed)
        if (_lastTime != _thisTime)
        {
            // Add force in direction of player
            GetComponent<Rigidbody>().AddForce((_player.transform.position - transform.position).normalized * Speed);
        }

        // Sets last time for time comparison in next frame
        _lastTime = _thisTime;
    }

    // If enemy collides with
    private void OnCollisionEnter(Collision c)
    {
        Debug.Log("collision");
        if (c.gameObject.layer == Ground)
        {
            GetComponent<AudioSource>().PlayOneShot(Thud);
            Debug.Log("ground");
        }
        else if (c.gameObject.layer == Bean)
        {
            GetComponent<AudioSource>().PlayOneShot(Hit);
            health--;
            
            Debug.Log("bean");
        }
        else if (c.gameObject.layer == Player)
        {
            Debug.Log("player");
            GetComponent<AudioSource>().PlayOneShot(Pop);

            c.gameObject.GetComponent<Health>().Remove();
        }
        
        if (health < 1)
        {
            Handler.Death();
            Destroy(gameObject);
        }
    }
}