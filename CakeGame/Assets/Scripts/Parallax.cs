using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Parallax : MonoBehaviour
{
    private float lenght, startposX, startposY;
    public GameObject cam;
    public float parallaxEffectX;
    public float parallaxEffectY;
    private float p;
    private float p2;
 
    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        lenght = GetComponent<SpriteRenderer>().bounds.size.y;
    }
 
    // Update is called once per frame
    void Update()
    {
        //float dist = (cam.transform.position.y * parallaxEffect);

        //transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

        if (Input.GetButton("test"))
        {
            p2 = p + 1;
        }

        p = p + (p2 - p) * Time.deltaTime * 5;
 
        //transform.position = new Vector3(transform.position.x, startpos - parallaxEffect*p, transform.position.z);
        transform.position = new Vector3(startposX - parallaxEffectX * p, startposY - parallaxEffectY * p, transform.position.z);
    }

    public void moveObject()
    {
        p2 = p + 1;
    }
}