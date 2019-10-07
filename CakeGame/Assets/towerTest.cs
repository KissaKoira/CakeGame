using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("test"))
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, 1));
        }
    }
}
