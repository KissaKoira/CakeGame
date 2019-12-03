using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    GameObject anchor;
    Rigidbody2D anchorRigid;
    float p2 = -4f;

    // Start is called before the first frame update
    void Start()
    {
        anchor = GameObject.Find("Ground");
        anchorRigid = anchor.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anchorRigid.MovePosition(new Vector2(anchor.transform.position.x, anchor.transform.position.y + (p2 - anchor.transform.position.y) * Time.deltaTime * 5));
    }

    public void moveCamera(GameObject newAnchor)
    {
        anchor = newAnchor;
        anchorRigid = anchor.GetComponent<Rigidbody2D>();
        p2 = anchor.transform.position.y - 1;
    }
}
