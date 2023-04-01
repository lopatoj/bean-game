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

    [SerializeField] private ParticleSystem Explode;

    [SerializeField] private AudioClip Thud;
    
    [SerializeField] private AudioClip Hit;
    
    [SerializeField] private AudioClip Pop;

    [SerializeField] private AudioClip Explosion;

    // Player object in scene
    private GameObject _player;
    private Rigidbody _rigidbody;

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
        _rigidbody = GetComponent<Rigidbody>();
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
            _rigidbody.AddForce((_player.transform.position - transform.position).normalized * Speed);
        }

        // Sets last time for time comparison in next frame
        _lastTime = _thisTime;
    }

    // If enemy collides with
    private void OnCollisionEnter(Collision c)
    {
        var a = GetComponent<AudioSource>();
        
        if (health == 0)
        {
            health = -1;
            Debug.Log("kill");
            Handler.Death();
            GetComponent<MeshRenderer>().enabled = false;
            a.PlayOneShot(Explosion);
            Explode.Play();
            Destroy(this.gameObject, 1f);
        }
        else
        {
            if (c.gameObject.name == "Terrain")
            {
                a.PlayOneShot(Thud, .2f);
            }
            else if (c.gameObject.name == "Player")
            {
                a.PlayOneShot(Pop, .7f);

                c.gameObject.GetComponent<Health>().Remove();
            }
            else if (c.gameObject.CompareTag("Bean"))
            {
                Debug.Log("Health now: " + health);
                a.PlayOneShot(Hit);
                health--;
            }
        }
    }
}