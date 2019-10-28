using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pointFade : MonoBehaviour
{
    float counter;

    private void Start()
    {
        counter = 1;
    }

    void Update()
    {
        counter -= Time.deltaTime;

        if(counter < 0)
        {
            Debug.Log(this.gameObject + " destroyed");
            Destroy(this.gameObject);
        }
    }
}
