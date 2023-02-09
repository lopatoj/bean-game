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
        Eyes.fieldOfView = Global.fov;
        paused = false;

        // Keeps mouse cursor in center of screen & hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if(!paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            paused = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync(1);

            paused = false;
        }
    }
}
