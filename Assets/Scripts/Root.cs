using UnityEngine;
using UnityEngine.SceneManagement;

public class Root : MonoBehaviour
{
    [SerializeField] private Camera Eyes;

    [SerializeField] private Global.Global Global;

    private bool _paused;

    private void Start()
    {
        _paused = false;

        // Keeps mouse cursor in center of screen & hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Eyes.fieldOfView = Global.fov;

        if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            _paused = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            _paused = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    private void Pause()
    {
        if (!_paused)
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        else
            SceneManager.UnloadSceneAsync(1);
    }
}