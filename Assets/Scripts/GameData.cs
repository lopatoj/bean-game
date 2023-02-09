using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    public float volume;
    public float fov;
    public float sens;

    public GameData(float v, float f, float s)
    {
        this.volume = v;
        this.fov = f;
        this.sens = s;
    }
}
