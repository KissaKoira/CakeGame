using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    GameObject gameController;

    public GameObject cakeSplat;

    public GameObject canvas;
    public GameObject points;

    private GameObject cakeBody;
    public GameObject lastCake;

    float cakeOffset;

    private GameObject[] cakes;

    private GameObject anchor;

    private float counter = 0;

    private void Awake()
    {
        cakeBody = this.transform.parent.gameObject;

        gameController = GameObject.Find("GameController");

        lastCake = gameController.GetComponent<GameController>().getLastCake();

        canvas = GameObject.Find("Canvas");

        anchor = GameObject.Find("Ground");
    }

    private void limitCakes(GameObject newCake)
    {
        cakes = gameController.GetComponent<GameController>().getCakes();

        for (int i = cakes.Length - 1; i >= 0; i--)
        {

            Debug.Log(cakes.Length + " " + i + " " + cakes[i]);

            if(i == 0)
            {
                cakes[i] = newCake;
            }
            else if(cakes[i] != null)
            {
                if (i == cakes.Length - 1)
                {

                    Destroy(cakes[i].gameObject);
                    cakes[i] = cakes[i - 1];
                    setAnchor(cakes[i]);
                }
                else
                {
                    cakes[i] = cakes[i - 1];
                }
            }
            else
            {
                if (cakes[i - 1] != null)
                {
                    cakes[i] = cakes[i - 1];
                }
            }
        }

        gameController.GetComponent<GameController>().setCakes(cakes);
    }

    private void setAnchor(GameObject newAnchor)
    {
        anchor = newAnchor;
        anchor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        anchor.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false;

        if (lastCake != null)
        {
            cakeOffset = Mathf.Abs(cakeBody.transform.position.x - lastCake.transform.position.x);
            Debug.Log(cakeOffset);
        }

        if (lastCake == null || cakeOffset < 1f)
        {
            limitCakes(cakeBody);

            //plays the bounce animation
            cakeBody.GetComponentInChildren<Animator>().SetTrigger("bounce");

            GameObject thisSplat = Instantiate(cakeSplat, cakeBody.transform.position, Quaternion.identity);
            thisSplat.transform.position -= new Vector3(0, 0.3f, 0);
            thisSplat.transform.eulerAngles = new Vector3(-90, 0, 0);

            //points
            Instantiate(points, canvas.transform);

            cakeBody.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

            if (lastCake != null)
            {
                cakeBody.AddComponent<FixedJoint2D>();
                cakeBody.GetComponent<FixedJoint2D>().connectedBody = lastCake.GetComponent<Rigidbody2D>();
            }
            else
            {
                cakeBody.AddComponent<FixedJoint2D>();
                cakeBody.GetComponent<FixedJoint2D>().connectedBody = GameObject.Find("Ground").GetComponent<Rigidbody2D>();
            }

            if(cakeBody.transform.position.y > -2)
            {
                GameObject.Find("Main Camera").GetComponent<cameraController>().moveCamera(anchor);

                var objects = FindObjectsOfType<Parallax>();

                for(int i = 0; i < objects.Length; i++)
                {
                    objects[i].moveObject();
                }
            }

            gameController.GetComponent<GameController>().setLastCake(cakeBody);            
        }
        else
        {
            cakeBody.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            cakeBody.transform.GetChild(1).GetComponent<Animator>().SetTrigger("fade");
        }
    }
}
