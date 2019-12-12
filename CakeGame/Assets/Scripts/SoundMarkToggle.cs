using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMarkToggle : MonoBehaviour
{
    void Awake()
    {
        if (this.gameObject.name == "Music")
        {
            GetComponent<Toggle>().isOn = !FindObjectOfType<AudioManager>().musicMute;
            GetComponent<Toggle>().onValueChanged.AddListener((val) => { FindObjectOfType<AudioManager>().ToggleMusicMute(); });
        }
        if (this.gameObject.name == "Sound")
        {
            GetComponent<Toggle>().isOn = !FindObjectOfType<AudioManager>().soundMute;
            GetComponent<Toggle>().onValueChanged.AddListener((val) => { FindObjectOfType<AudioManager>().ToggleSoundMute(); });
        }
    }
}
