using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splatController : MonoBehaviour
{
    float counter;

    private void Start()
    {
        counter = 1;
    }

    void Update()
    {
        if(counter > 0)
        {
            counter -= Time.deltaTime;
        }

        if(counter < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
