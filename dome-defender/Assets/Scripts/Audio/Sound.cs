using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string Name;

    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 1f;
    [Range(.1f, 3f)]
    public float Pitch = 1f;

    [HideInInspector]
    public AudioSource Source;
}
