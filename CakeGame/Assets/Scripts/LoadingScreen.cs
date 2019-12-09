using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    Image loadingFill;
    AsyncOperation asyncLoad;

    private void Start()
    {
        loadingFill = GameObject.Find("Fill").GetComponent<Image>();
        asyncLoad = FindObjectOfType<SceneController>().async;
    }

    // Update is called once per frame
    void Update()
    {
        loadingFill.fillAmount = asyncLoad.progress;
        //Debug.Log(loadingFill.fillAmount);
        if (asyncLoad.isDone)
        {
            Destroy(this);
        }
    }
}
