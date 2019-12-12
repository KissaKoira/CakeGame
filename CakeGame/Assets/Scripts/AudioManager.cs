using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Music[] music;

    public AudioMixerGroup soundMixer;
    public AudioMixerGroup musicMixer;

    public bool soundMute;
    public bool musicMute;

    public Music currentMusic;
    private Music nextMusic;

    public string initialMusic;

    public static AudioManager instance;

    void Awake()
    {
        //Make AudioManager single persistent instance
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        currentMusic = null;

        //Create AudioSources from sounds array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = soundMixer;
        }
        foreach (Music s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = !s.useLoopPoint;

            s.source.outputAudioMixerGroup = musicMixer;
        }
    }

    void Start()
    {
        if (initialMusic != "")
        {
            PlayMusic(initialMusic);
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = s.volume;
        s.source.volume -= (Random.Range(0f, 1f) * s.volumeRandom);
        s.source.pitch = s.pitch;
        s.source.pitch += (Random.Range(-1f, 1f) * s.pitchRandom);
        s.source.Play();
    }

    public void PlayMusic (string name)
    {
        Music nextMusic = Array.Find(music, music => music.name == name);
        if (nextMusic == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }
        if (nextMusic != currentMusic)
        {
            nextMusic.source.volume = 0;
            nextMusic.source.time = currentMusic != null && nextMusic.syncTime ? currentMusic.source.time : 0f;
            nextMusic.source.Play();

            nextMusic.source.DOFade(nextMusic.volume, 1f);
            if (currentMusic != null)
            {
                Music prevMusic = currentMusic;
                prevMusic.source.DOFade(0f, 1f).OnComplete(() => {
                    prevMusic.source.Stop();
                });
            }
            currentMusic = nextMusic;
        }
    }

    public void ToggleSoundMute()
    {
        if (soundMute)
        {
            soundMixer.audioMixer.SetFloat("volumeSounds", 0f);
            soundMute = false;
        }
        else
        {
            soundMixer.audioMixer.SetFloat("volumeSounds", -80f);
            soundMute = true;
        }
    }

    public void ToggleMusicMute()
    {
        if (musicMute)
        {
            musicMixer.audioMixer.SetFloat("volumeMusic", 0f);
            musicMute = false;
        }
        else
        {
            musicMixer.audioMixer.SetFloat("volumeMusic", -80f);
            musicMute = true;
        }
    }

    public void Update()
    {
        if (currentMusic != null && currentMusic.useLoopPoint && currentMusic.source.time >= currentMusic.source.clip.length)
        {
            currentMusic.source.Play();
            currentMusic.source.time = currentMusic.loopPoint;
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleSoundMute();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleMusicMute();
        }
    }
}
