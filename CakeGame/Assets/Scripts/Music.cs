using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Music
{
    public string name;

    public AudioClip clip;

    [Header("Sound Settings")]

    [Range(0f, 1f)]
    public float volume = 1;

    public bool syncTime;
    public bool useLoopPoint;
    public float loopPoint;

    [HideInInspector]
    public AudioSource source;
}
