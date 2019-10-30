using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    GameObject ground;
    float p2 = -4.86f;

    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.Find("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        ground.GetComponent<Rigidbody2D>().MovePosition(new Vector2(0, ground.transform.position.y + (p2 - ground.transform.position.y) * Time.deltaTime * 5));
    }

    public void moveCamera()
    {
        p2 = ground.transform.position.y - 1;
    }
}
