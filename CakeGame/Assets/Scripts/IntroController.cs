﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public float introTime;
    public string sceneAfter;

    // Update is called once per frame
    void Update()
    {
        introTime -= Time.deltaTime;
        if (introTime < 0)
        {
            SceneManager.LoadScene(sceneAfter);
        }
    }
}
