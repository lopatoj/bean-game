using UnityEngine;
using UnityEngine.SceneManagement;

public class Root : MonoBehaviour
{
    // Objects from game scene that need to be referenced by this class
    [SerializeField] private Camera Eyes;

    [SerializeField] private Global.Global Global;

    [SerializeField] private Canvas HUD;

    // Private value that changes depending on keyboard input
    private bool _paused;

    // Runs once before first frame
    private void Start()
    {
        _paused = false;

        // Keeps mouse cursor in center of screen & hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Runs once every frame
    private void Update()
    {
        // Sets in game field of view to the settings value for field of view
        Eyes.fieldOfView = Global.fov;

        // If pause menu is loaded
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            // Allow cursor to be visible and to move
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            HUD.enabled = false;
            
            // Halt in-game time
            Time.timeScale = 0;
            _paused = true;
        }
        else
        {
            // Hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            HUD.enabled = true;
            
            // Resume in-game time
            Time.timeScale = 1;
            _paused = false;
        }

        // If escape key pressed, pause game
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    // Runs when escape key pressed
    private void Pause()
    {
        // If not paused, then load pause menu, otherwise unload the pause menu
        if (!_paused)
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        else
            SceneManager.UnloadSceneAsync(1);
    }
}