using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public float volume;
    public float fov;
    public float sensitivity;

    public GameData(float v, float f, float s)
    {
        volume = v;
        fov = f;
        sensitivity = s;
    }

    public GameData(Global.Global g)
    {
        volume = g.volume;
        fov = g.fov;
        sensitivity = g.sensitivity;
    }

    public GameData()
    {
        volume = 1f;
        fov = 90f;
        sensitivity = 3f;
    }
}