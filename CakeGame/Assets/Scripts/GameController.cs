using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cakePref;
    public GameObject cablePref;
    private GameObject newCable;
    private GameObject newCake;

    private Vector2 cablePos;
    private Quaternion cableRot;

    public GameObject anchor;

    public GameObject cakeWrapper;
    private Vector3 wrapperScale;

    private float respawnCounter = 0;

    void Start()
    {
        wrapperScale = cakeWrapper.transform.localScale;

        respawnCounter = -1f;
    }

    void Update()
    {
        if (Input.GetButtonDown("drop"))
        {
            if (newCable != null)
            {
                Destroy(newCable);
            }

            if (newCake != null)
            {
                newCake.transform.SetParent(null);
                newCake.GetComponent<HingeJoint2D>().enabled = false;
            }

            newCable = Instantiate(cablePref, new Vector3(0, 3.6f, 0), Quaternion.identity);
            newCable.transform.SetParent(cakeWrapper.transform);
            newCable.GetComponent<HingeJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();

            newCake = Instantiate(cakePref, new Vector3(0, 2.45f, 0), Quaternion.identity);
            newCake.transform.SetParent(cakeWrapper.transform);
            newCake.GetComponent<HingeJoint2D>().connectedBody = newCable.GetComponent<Rigidbody2D>();
            newCake.GetComponent<BoxCollider2D>().enabled = false;

            cakeWrapper.transform.localScale = new Vector3(0.5f, 0.5f, wrapperScale.y);

            respawnCounter = 2;
        }

        if (Input.GetButtonDown("test"))
        {
            if(newCake != null)
            {
                newCake.GetComponent<Rigidbody2D>().velocity += new Vector2(2f, 0);
            }
        }

        if (respawnCounter > 0)
        {
            respawnCounter -= Time.deltaTime;

            cakeWrapper.transform.localScale += new Vector3(Time.deltaTime * 0.25f, Time.deltaTime * 0.25f, 0);

            if (respawnCounter < 0)
            {
                cakeWrapper.transform.localScale = wrapperScale;
                newCake.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}