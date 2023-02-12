using System;
using UnityEngine;

[Serializable]
public class GameData
{
    // Settings values
    public float volume;
    public float fov;
    public float sensitivity;

    // Creates game data object from three separate values
    public GameData(float v, float f, float s)
    {
        volume = v;
        fov = f;
        sensitivity = s;
    }

    // Creates game data object from Global object
    public GameData(Global.Global g)
    {
        volume = g.volume;
        fov = g.fov;
        sensitivity = g.sensitivity;
    }

    // Creates game data object with default values
    public GameData()
    {
        volume = 1f;
        fov = 90f;
        sensitivity = 3f;
    }
}