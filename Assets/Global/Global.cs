using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Global")]
public class Global : ScriptableObject
{
    public float volume;
    public float fov;
    public float sensitivity;

    public Global(float v, float f, float s)
    {
        this.volume = v;
        this.fov = f;
        this.sensitivity = s;
    }

    public Global()
    {
        this.volume = 1f;
        this.fov = 90f;
        this.sensitivity = 2f;
    }
}
