using System;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    public float fov;
    public float sens;
    public float volume;

    public GameData(float v, float f, float s)
    {
        volume = v;
        fov = f;
        sens = s;
    }
}