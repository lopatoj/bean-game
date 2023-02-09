using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Root : MonoBehaviour
{
    [SerializeField]
    private Global Global;
    [SerializeField]
    private Camera Eyes;

    private bool paused;

    void Start()
    {
        paused = false;

        // Keeps mouse cursor in center of screen & hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Eyes.fieldOfView = Global.fov;

        if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            paused = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if(!paused)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
        else
        { 
            SceneManager.UnloadSceneAsync(1);
        }
    }
}
