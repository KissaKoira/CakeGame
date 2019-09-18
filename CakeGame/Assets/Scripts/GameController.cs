using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //object prefabs
    public GameObject cakePref;
    public GameObject cablePref;
    public GameObject anchorPref;
    public GameObject wrapperPref;

    //left cake's objects
    private GameObject leftCakeWrapper;
    private GameObject leftAnchor;
    private GameObject leftCable;
    private GameObject leftCake;

    //right cake's objects
    private GameObject rightCakeWrapper;
    private GameObject rightAnchor;
    private GameObject rightCable;
    private GameObject rightCake;

    //current cake's objects
    private GameObject currentWrapper;
    private GameObject currentAnchor;
    private GameObject currentCable;
    private GameObject currentCake;

    //left cake's object positions
    private Vector3 leftCablePos;
    private Vector3 leftCakePos;
    private Vector3 leftAnchorPos;

    //right cake's object positions
    private Vector3 rightCablePos;
    private Vector3 rightCakePos;
    private Vector3 rightAnchorPos;

    //whether or not a new cake's direction has changed
    private bool anchorDirectionChanged = false;

    //counter for timing respawn events
    private float respawnCounter = 0;

    //creates a new cake if a cake is missing
    private void createCake()
    {
        if(rightCake == null)
        {
            rightCakeWrapper = Instantiate(wrapperPref, rightAnchorPos, Quaternion.identity);

            rightAnchor = Instantiate(anchorPref, rightAnchorPos, Quaternion.identity);
            rightAnchor.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            rightAnchor.GetComponent<Rigidbody2D>().freezeRotation = true;

            rightCable = Instantiate(cablePref, rightCablePos, Quaternion.identity);
            rightCable.transform.SetParent(rightCakeWrapper.transform);
            rightCable.GetComponent<HingeJoint2D>().connectedBody = rightAnchor.GetComponent<Rigidbody2D>();

            rightCake = Instantiate(cakePref, rightCakePos, Quaternion.identity);
            rightCake.transform.SetParent(rightCakeWrapper.transform);
            rightCake.GetComponent<HingeJoint2D>().connectedBody = rightCable.GetComponent<Rigidbody2D>();
            rightCake.GetComponent<BoxCollider2D>().enabled = false;

            rightCakeWrapper.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }

        if(leftCake == null)
        {
            leftCakeWrapper = Instantiate(wrapperPref, leftAnchorPos, Quaternion.identity);

            leftAnchor = Instantiate(anchorPref, leftAnchorPos, Quaternion.identity);
            leftAnchor.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            leftAnchor.GetComponent<Rigidbody2D>().freezeRotation = true;

            leftCable = Instantiate(cablePref, leftCablePos, Quaternion.identity);
            leftCable.transform.SetParent(leftCakeWrapper.transform);
            leftCable.GetComponent<HingeJoint2D>().connectedBody = leftAnchor.GetComponent<Rigidbody2D>();

            leftCake = Instantiate(cakePref, leftCakePos, Quaternion.identity);
            leftCake.transform.SetParent(leftCakeWrapper.transform);
            leftCake.GetComponent<HingeJoint2D>().connectedBody = leftCable.GetComponent<Rigidbody2D>();
            leftCake.GetComponent<BoxCollider2D>().enabled = false;

            leftCakeWrapper.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
    }

    //swipecontrol variables
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public int Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    return 0;
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    return 0;
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if(currentCake == null)
                    {
                        currentCable = rightCable;
                        currentCake = rightCake;
                        currentAnchor = rightAnchor;
                        currentWrapper = rightCakeWrapper;

                        rightCake = null;

                        respawnCounter = 1;
                        currentAnchor.GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);
                    }

                    return -1;
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if (currentCake == null)
                    {
                        currentCable = leftCable;
                        currentCake = leftCake;
                        currentAnchor = leftAnchor;
                        currentWrapper = leftCakeWrapper;

                        leftCake = null;

                        respawnCounter = 1;
                        currentAnchor.GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 0);
                    }

                    return 1;
                }
            }
        }
        return 0;
    }

    void Start()
    {
        //the distance the cakes are set off the middle
        float cakeOffset = 1.2f;

        //sets the object positions at the start of the program
        leftCablePos = new Vector3(cakeOffset * -1, 3.9f, 0);
        leftCakePos = new Vector3(cakeOffset * -1, 2.45f, 0);
        leftAnchorPos = new Vector3(cakeOffset * -1, 5.05f, 0);

        rightCablePos = new Vector3(cakeOffset, 3.9f, 0);
        rightCakePos = new Vector3(cakeOffset, 2.45f, 0);
        rightAnchorPos = new Vector3(cakeOffset, 5.05f, 0);

        //creates the first cakes
        createCake();
    }

    void dropCake(GameObject cake)
    {
        float rotation;
        float velocity = 0;

        if(cake.transform.rotation.z < 0)
        {
            rotation = 0.3f;
        }
        else
        {
            rotation = -0.3f;
        }

        cake.transform.Rotate(new Vector3(0, 0, rotation));

        if(Mathf.Abs(cake.transform.rotation.z) < 0.01f)
        {
            cake.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        velocity -= Time.deltaTime * 10;

        cake.GetComponent<Rigidbody2D>().velocity += new Vector2(0, velocity);
    }

    GameObject droppingCake;

    void Update()
    {
        //swipe controls
        Swipe();

        //rotates the dropping cake 
        if(droppingCake != null)
        {
            dropCake(droppingCake);
        }
        
        //if there is no ongoing respawn event
        if(respawnCounter <= 0)
        {
            //drops the current cake and creates a new one
            if (Input.GetButtonDown("drop"))
            {
                if(currentCake != null)
                {
                    Destroy(currentWrapper);
                    Destroy(currentAnchor);
                    Destroy(currentCable);

                    currentCake.transform.SetParent(null);
                    currentCake.GetComponent<HingeJoint2D>().enabled = false;
                    currentCake.GetComponent<BoxCollider2D>().enabled = true;
                    currentCake.transform.GetChild(1).gameObject.SetActive(true);

                    currentCake.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                    currentCake.GetComponent<Rigidbody2D>().freezeRotation = true;

                    droppingCake = currentCake;

                    currentCake = null;
                }

                createCake();
            }

            //brings in the right cake
            if (Input.GetAxisRaw("Horizontal") > 0 && currentCake == null)
            {
                currentCable = rightCable;
                currentCake = rightCake;
                currentAnchor = rightAnchor;
                currentWrapper = rightCakeWrapper;

                rightCake = null;

                respawnCounter = 1;
                currentAnchor.GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);
            }
            //brings in the left cake
            else if (Input.GetAxisRaw("Horizontal") < 0 && currentCake == null)
            {
                currentCable = leftCable;
                currentCake = leftCake;
                currentAnchor = leftAnchor;
                currentWrapper = leftCakeWrapper;

                leftCake = null;

                respawnCounter = 1;
                currentAnchor.GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 0);
            }
        }
        
        //scales the incoming cake and enables the collider
        if (respawnCounter > 0)
        {
            respawnCounter -= Time.deltaTime;

            currentWrapper.transform.localScale += new Vector3(Time.deltaTime * 0.5f, Time.deltaTime * 0.5f, 0);

            if (respawnCounter < 0)
            {
                currentWrapper.transform.localScale = new Vector3(1, 1, 1);
                currentCake.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        if(currentAnchor != null)
        {
            if (respawnCounter < 0)
            {
                anchorDirectionChanged = false;

                //freezes the current cake's anchor once it reaches the middle
                if (currentAnchor.transform.position.x < 0.1f && currentAnchor.transform.position.x > -0.1f)
                {
                    respawnCounter = 0;
                    currentAnchor.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
            
            //changes the current anchor's direction 0.5 seconds before the end of the respawn event
            if ((respawnCounter < 0.5f && respawnCounter > 0) && anchorDirectionChanged == false)
            {
                anchorDirectionChanged = true;

                if(currentAnchor.GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    currentAnchor.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
                }
                else
                {
                    currentAnchor.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
                }
            }
        }
    }
}