using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Sprite[] cakeSprites;
    public Sprite[] outlineSprites;
    public int[] cakePoints;
    public Color[] cakeColors;

    //object prefabs
    public GameObject cakePref;

    //left cake's objects
    private GameObject leftCake;

    //right cake's objects
    private GameObject rightCake;

    //current cake's objects
    private GameObject currentCake;

    private string cakeFrom = "";

    //cake's object positions
    private Vector3 cakePos;

    private bool animationActive = false;

    public GameObject pointMeter;
    public float points;

    public float frenzy = 0;

    public GameObject firstCake;

    //creates a new cake if a cake is missing
    private void createCake()
    {
        int random = (int)Random.Range(0, 127.999f);
        int index = 0;

        if (random > 64)
        {
            index = 0;
        }
        else if (random > 32)
        {
            index = 1;
        }
        else if (random > 16)
        {
            index = 2;
        }
        else if (random > 8)
        {
            index = 3;
        }
        else if (random > 4)
        {
            index = 4;
        }
        else if (random > 2)
        {
            index = 5;
        }
        else if (random > 1)
        {
            index = 6;
        }
        else
        {
            index = 7;
        }

        if (rightCake == null)
        {
            rightCake = Instantiate(cakePref, cakePos, Quaternion.identity);

            GameObject newCake = rightCake.transform.GetChild(2).gameObject;
            newCake.GetComponent<cakeController>().cake = cakeSprites[index];
            newCake.GetComponent<cakeController>().outline = outlineSprites[index];
            newCake.GetComponent<cakeController>().points = cakePoints[index];
            newCake.GetComponent<cakeController>().colors = cakeColors[index];

            rightCake.GetComponent<Animator>().SetTrigger("spawnRight");
        }

        if(leftCake == null)
        {
            leftCake = Instantiate(cakePref, cakePos, Quaternion.identity);

            GameObject newCake = leftCake.transform.GetChild(2).gameObject;
            newCake.GetComponent<cakeController>().cake = cakeSprites[index];
            newCake.GetComponent<cakeController>().outline = outlineSprites[index];
            newCake.GetComponent<cakeController>().points = cakePoints[index];
            newCake.GetComponent<cakeController>().colors = cakeColors[index];

            leftCake.GetComponent<Animator>().SetTrigger("spawnLeft");
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
                if ((currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) && currentCake == null)
                {
                    currentCake = rightCake;

                    rightCake = null;

                    currentCake.GetComponent<Animator>().SetTrigger("moveRight");

                    cakeFrom = "right";

                    createCake();
                }
                //swipe right
                if ((currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) && currentCake == null)
                {
                    currentCake = leftCake;

                    leftCake = null;

                    currentCake.GetComponent<Animator>().SetTrigger("moveLeft");

                    cakeFrom = "left";

                    createCake();
                }
            }
        }
        return 0;
    }

    void Start()
    {
        //sets the object positions at the start of the program
        cakePos = new Vector3(0, 7f, 0);

        //creates the first cakes
        createCake();

        currentCake = firstCake;
    }

    GameObject droppingCake;

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

        cake.transform.Rotate(new Vector3(0, 0, rotation * Time.deltaTime * 500));

        if(Mathf.Abs(cake.transform.rotation.z) < 0.03f)
        {
            cake.transform.rotation = new Quaternion(0, 0, 0, 0);
            droppingCake = null;
        }

        velocity -= Time.deltaTime * 10;

        cake.GetComponent<Rigidbody2D>().velocity += new Vector2(0, velocity);
    } 

    void Update()
    {
        pointMeter.GetComponent<TextMeshProUGUI>().SetText(points.ToString());

        if(frenzy > 0 && frenzy < 100)
        {
            frenzy -= Time.deltaTime * 3;
        }
        else if (frenzy >= 100)
        {

        }
        else
        {
            frenzy = 0;
        }

        //swipe controls
        Swipe();

        //rotates the dropping cake 
        if(droppingCake != null)
        {
            dropCake(droppingCake);
        }
        
        //drops the current cake and creates a new one
        if (Input.GetButtonDown("drop"))
        {
            if(currentCake != null && !animationActive)
            {
                animationActive = true;

                //changes currentCake from cakeWrapper to Cake
                GameObject wrapper = currentCake;
                currentCake = wrapper.transform.GetChild(2).gameObject;

                Debug.Log(currentCake);

                currentCake.transform.SetParent(null);
                currentCake.GetComponent<HingeJoint2D>().enabled = false;
                currentCake.GetComponent<BoxCollider2D>().enabled = true;
                
                currentCake.transform.GetChild(0).gameObject.SetActive(true);

                currentCake.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                currentCake.GetComponent<Rigidbody2D>().freezeRotation = true;

                droppingCake = currentCake;

                Destroy(wrapper);
                currentCake = null;
            }
        }

        //brings in the right cake
        if (Input.GetAxisRaw("Horizontal") > 0 && currentCake == null)
        {
            currentCake = rightCake;

            rightCake = null;

            currentCake.GetComponent<Animator>().SetTrigger("moveRight");

            cakeFrom = "right";

            createCake();
        }
        //brings in the left cake
        else if (Input.GetAxisRaw("Horizontal") < 0 && currentCake == null)
        {
            currentCake = leftCake;

            leftCake = null;

            currentCake.GetComponent<Animator>().SetTrigger("moveLeft");

            cakeFrom = "left";

            createCake();
        }

        if (Input.GetButtonDown("test"))
        {
            Debug.Log("test");
        }
    }

    public void disableAnimator()
    {
        Debug.Log("animator disabled " + currentCake.transform.GetChild(2).gameObject.name);

        animationActive = false;

        if(cakeFrom == "right")
        {
            currentCake.transform.GetChild(2).gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(-5, 0);
        }
        else
        {
            currentCake.transform.GetChild(2).gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(5, 0);
        }
    }

    GameObject lastCake;

    public void setLastCake(GameObject cake)
    {
        lastCake = cake;
    }

    public GameObject getLastCake()
    {
        return lastCake;
    }

    private GameObject[] cakes = new GameObject[10];

    public void setCakes(GameObject[] newCakes)
    {
        cakes = newCakes;
    }

    public GameObject[] getCakes()
    {
        return cakes;
    }

    private int comboCounter;

    public void setComboCounter(int num)
    {
        comboCounter = num;
    }

    public int getComboCounter()
    {
        return comboCounter;
    }

    private int health = 3;

    public void setHealth(int num)
    {
        health = num;
    }

    public int getHealth()
    {
        return health;
    }
}