using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BeanCollecting : MonoBehaviour
{
    [SerializeField] private Throw Camera;
    
    [SerializeField] private LayerMask Bean;

    [SerializeField] private AudioClip Grab;

    private void Update()
    {
        BeanCheck();
    }

    private void BeanCheck()
    {
        if (Physics.SphereCast(transform.position, 3f, transform.forward, out RaycastHit r, 3f, Bean))
        {
            if (r.transform.gameObject.GetComponent<Timer>().time > 1f) {
                GetComponent<AudioSource>().PlayOneShot(Grab);
                
                Camera.Add();
                Destroy(r.transform.gameObject);
            }
        }
    }
}
