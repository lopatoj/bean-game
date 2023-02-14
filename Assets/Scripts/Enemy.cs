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
        var a = GetComponent<AudioSource>();
        
        if (c.gameObject.name == "Terrain")
        {
            a.PlayOneShot(Thud, .2f);
        }
        else if (c.gameObject.name == "Bean")
        {
            Debug.Log("Bean");
            a.PlayOneShot(Hit);
            health--;
        }
        else if (c.gameObject.name == "Player")
        {
            a.PlayOneShot(Pop, .7f);

            c.gameObject.GetComponent<Health>().Remove();
        }
        
        if (health < 1)
        {
            Debug.Log("kill");
            Handler.Death();
            Destroy(this.gameObject);
        }
    }
}