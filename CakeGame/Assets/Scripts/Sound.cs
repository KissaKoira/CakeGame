﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Header("Sound Settings")]

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0f, 1f)]
    public float volumeRandom;
    [Range(.1f, 3f)]
    public float pitch = 1;
    [Range(0f, 1f)]
    public float pitchRandom;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
