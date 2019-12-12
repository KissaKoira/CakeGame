using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string currentSceneName;
    public static SceneController instance;
    public GameObject loadingPrefab;
    public AsyncOperation async;

    // Start is called before the first frame update
    void Awake()
    {
        //Make single persistent instance
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneAsync(int loadSceneIndex)
    {
        Instantiate(loadingPrefab);
        instance.StartCoroutine(instance.AsyncLoad(loadSceneIndex));
    }

    IEnumerator AsyncLoad(int index)
    {
        async = SceneManager.LoadSceneAsync(index);
        Debug.Log("Start async scene loading");
        while (!async.isDone)
        {
            yield return null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded scene: " + currentSceneName);
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrentScene();
        }
    }
}
