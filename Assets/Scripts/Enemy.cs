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

    private GameObject _player;

    private int _thisTime;
    private int _lastTime;

    public int health;
    public EnemyHandler Handler;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _thisTime = (int)Math.Floor(Time.time);
        
        if (_lastTime != _thisTime)
        {
            GetComponent<Rigidbody>().AddForce((_player.transform.position - transform.position).normalized * Speed);
        }

        _lastTime = _thisTime;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == Ground)
        {
            GetComponent<AudioSource>().PlayOneShot(Thud);
        }
        else if (c.gameObject.layer == Bean)
        {
            GetComponent<AudioSource>().PlayOneShot(Hit);
            health--;
            
            Debug.Log("bean");
        }
        else if (c.gameObject.layer == Player)
        {
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