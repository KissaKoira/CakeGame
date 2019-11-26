using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverEffects : MonoBehaviour
{
    public bool FeverState;
    private Animator anim;
    private AudioManager audio;
    private string prevMusic;
    public void Start()
    {
        anim = GetComponent<Animator>();
        audio = FindObjectOfType<AudioManager>();
        FeverState = false;
    }
    public void StartFever()
    {
        anim.SetBool("Fever", true);
        audio.Play("FeverStart");
        prevMusic = audio.currentMusic.name;
        audio.PlayMusic("FeverMarch");
        FeverState = true;
    }

    public void StopFever()
    {
        anim.SetBool("Fever", false);
        audio.PlayMusic(prevMusic);
        FeverState = false;
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
