using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverEffects : MonoBehaviour
{
    public bool FeverState;
    private Animator anim;
    //private AudioManager audio;
    private string prevMusic;
    private bool startTriggered = false;
    private bool endTriggered = false;
    public void Start()
    {
        anim = GetComponent<Animator>();
        //audio = FindObjectOfType<AudioManager>();
        FeverState = false;
    }
    public void StartFever()
    {
        if (startTriggered == false)
        {
            FindObjectOfType<AudioManager>().Play("FeverStart");
            anim.SetBool("Fever", true);
            prevMusic = FindObjectOfType<AudioManager>().currentMusic.name;
            FindObjectOfType<AudioManager>().PlayMusic("FeverMarch");
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
            FindObjectOfType<AudioManager>().PlayMusic(prevMusic);
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
