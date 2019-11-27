using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cakeController : MonoBehaviour
{
    public float points;
    public Sprite cake;
    public Sprite outline;

    private void Start()
    {
        transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cake;
        transform.GetChild(1).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = outline;
    }
}
