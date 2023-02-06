using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class General : MonoBehaviour
{
    public Global Global;
    public Camera Eyes;

    void Start()
    {
        Eyes.fieldOfView = Global.fov;

        // Keeps mouse cursor in center of screen & hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0;
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
