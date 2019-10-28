using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    GameObject gameController;

    private bool cakeBalance = true;
    private GameObject cakeBody;
    public GameObject lastCake;

    private void Awake()
    {
        cakeBody = this.transform.parent.gameObject;
    }

    private void Start()
    {
        gameController = GameObject.Find("GameController");

        lastCake = gameController.GetComponent<GameController>().getLastCake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cakeBalance)
        {
            //plays the bounce animation
            cakeBody.GetComponentInChildren<Animator>().SetTrigger("bounce");

            cakeBody.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

            if(lastCake != null)
            {
                cakeBody.AddComponent<FixedJoint2D>();
                cakeBody.GetComponent<FixedJoint2D>().connectedBody = lastCake.GetComponent<Rigidbody2D>();
            }

            gameController.GetComponent<GameController>().setLastCake(cakeBody);
        }
        else
        {
            //cake falls
        }
    }
}
