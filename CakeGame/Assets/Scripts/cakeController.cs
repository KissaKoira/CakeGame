using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cakeController : MonoBehaviour
{
    public float points;
    public Sprite cake;
    public Sprite outline;
    public Color colors;

    private void Start()
    {
        transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cake;
        transform.GetChild(1).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = outline;
    }
}
