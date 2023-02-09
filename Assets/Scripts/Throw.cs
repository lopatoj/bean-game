using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject bean;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetAxis("Fire1") == 1) {
            Launch();
        }
    }

    void Launch() {
        Instantiate(bean, new Vector3(0, 0, 0), transform.rotation);
    }
}