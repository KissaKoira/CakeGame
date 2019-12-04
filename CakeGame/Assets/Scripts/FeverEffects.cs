using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverEffects : MonoBehaviour
{
    public bool FeverState;
    private Animator anim;
    private AudioManager audio;
    private string prevMusic;
    private bool startTriggered = false;
    private bool endTriggered = false;
    public void Start()
    {
        anim = GetComponent<Animator>();
        audio = FindObjectOfType<AudioManager>();
        FeverState = false;
    }
    public void StartFever()
    {
        if (startTriggered == false)
        {
            audio.Play("FeverStart");
            anim.SetBool("Fever", true);
            prevMusic = audio.currentMusic.name;
            audio.PlayMusic("FeverMarch");
            FeverState = true;
        }
        startTriggered = true;
        endTriggered = false;
    }

    public void StopFever()
    {
        if (endTriggered == false)
        {
            anim.SetBool("Fever", false);
            audio.PlayMusic(prevMusic);
            FeverState = false;
            startTriggered = false;
        }
        endTriggered = true;
        startTriggered = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            if (FeverState == true)
            {
                StopFever();
            }
            else
            {
                StartFever();
            }
        }
    }
}
