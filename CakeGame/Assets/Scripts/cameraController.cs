using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
<<<<<<< HEAD
    GameObject anchor;
=======
    GameObject ground;
>>>>>>> 1ce407b4594cfdb90e7a8c6303a2bca271c99726
    float p2 = -4f;

    // Start is called before the first frame update
    void Start()
    {
        anchor = GameObject.Find("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        anchor.GetComponent<Rigidbody2D>().MovePosition(new Vector2(anchor.transform.position.x, anchor.transform.position.y + (p2 - anchor.transform.position.y) * Time.deltaTime * 5));
    }

    public void moveCamera(GameObject newAnchor)
    {
        anchor = newAnchor;
        p2 = anchor.transform.position.y - 1;
    }
}
