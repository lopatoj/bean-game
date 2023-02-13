using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BeanCollecting : MonoBehaviour
{
    // Objects from game scene that need to be referenced by this class
    [SerializeField] private Throw Camera;
    
    [SerializeField] private LayerMask Bean;

    [SerializeField] private AudioClip Grab;

    // Runs once every frame
    private void Update()
    {
        // Checks if bean is near player
        BeanCheck();
    }

    // Checks if bean is near player
    private void BeanCheck()
    {
        // If a bean is withing a sphere of radius 3 units to the player
        if (Physics.SphereCast(transform.position, 3f, transform.forward, out RaycastHit r, 3f, Bean))
        {
            // If the bean has existed for longer than a second, to prevent from picking up a bean that was recently thrown
            if (r.transform.gameObject.GetComponent<Timer>().time > 1f) {

                // Play a pick up sound
                GetComponent<AudioSource>().PlayOneShot(Grab);
                
                // Adds to bean count
                Camera.Add();

                // Ends the bean
                Destroy(r.transform.gameObject);
            }
        }
    }
}
