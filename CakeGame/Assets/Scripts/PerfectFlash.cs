using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectFlash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Flash");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
