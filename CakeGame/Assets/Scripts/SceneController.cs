using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string currentSceneName;

    public static SceneController instance;

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

    public void GoToScene(string newScene)
    {
        Debug.Log(newScene);
        if (SceneManager.GetActiveScene().name != newScene)
        {
            Debug.Log("Start loading scene: " + newScene);
            SceneManager.LoadScene(newScene, LoadSceneMode.Single);
            SceneManager.sceneLoaded += OnSceneLoaded;
            currentSceneName = newScene;
        }
    }

    public void ToGame()
    {
        //FindObjectOfType<LoadingScreen>().gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().PlayMusic("March1");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        currentSceneName = "Main";
    }

    public void ToMenu()
    {
        //FindObjectOfType<LoadingScreen>().gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().PlayMusic("Intro1");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        currentSceneName = "MainMenu";
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
