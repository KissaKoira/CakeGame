using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject positionReferenceObject;
    private float bgY;
    private int bgLvl = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bgY = positionReferenceObject.transform.position.y / positionReferenceObject.GetComponent<Parallax>().parallaxEffectY;
        Debug.Log(FindObjectOfType<AudioManager>().currentMusic.name);
        //Debug.Log(bgY);

        if (bgLvl == 0 && bgY < -10f)
        {
            FindObjectOfType<AudioManager>().PlayMusic("March2");
            bgLvl = 1;
        }
        if (bgLvl == 1 && bgY < -75f)
        {
            FindObjectOfType<AudioManager>().PlayMusic("March3");
            bgLvl = 2;
        }
    }
}
