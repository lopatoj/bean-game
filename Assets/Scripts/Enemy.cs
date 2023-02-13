using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Objects from game scene that need to be referenced by this class
    [SerializeField] private float Speed;

    private GameObject _player;

    private int _thisTime;
    private int _lastTime;

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
}