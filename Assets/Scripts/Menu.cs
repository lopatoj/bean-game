using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Objects from game scene that need to be referenced by this class
    [SerializeField] private Slider FOVSlider;

    [SerializeField] private TextMeshProUGUI FOVSliderValue;

    [SerializeField] private Global.Global Global;

    [SerializeField] private Slider SensSlider;

    [SerializeField] private TextMeshProUGUI SensSliderValue;

    [SerializeField] private Slider VolumeSlider;

    [SerializeField] private TextMeshProUGUI VolumeSliderValue;

    // Private game data value containing saved data
    private GameData _game;

    // Runs once before first frame
    private void Start()
    {
        // Load data from settings text file
        _game = SaveSystem.Load();

        // Set global variables to settings values
        Global.volume = _game.volume;
        Global.fov = _game.fov;
        Global.sensitivity = _game.sensitivity;

        // Sets settings menu initial values to global values
        VolumeSlider.value = Global.volume;
        FOVSlider.value = Global.fov;
        SensSlider.value = Global.sensitivity;
    }

    // Runs once every frame
    private void Update()
    {
        // Set global variable values to settings menu values
        Global.volume = VolumeSlider.value;
        Global.fov = (int) FOVSlider.value;
        Global.sensitivity = SensSlider.value;

        // Change text elements to display global settings values
        VolumeSliderValue.text = $"{Global.volume:0.0}";
        FOVSliderValue.text = Global.fov + "";
        SensSliderValue.text = $"{Global.sensitivity:0.0}";

        // Change volume to global variable volume value
        AudioListener.volume = Global.volume;
    }

    // Opens main scene & saves settings to settings text file
    public void StartGame()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        SaveSystem.Save(Global);
    }

    // Saves settings to settings text file & closes application
    public void QuitGame()
    {

        SaveSystem.Save(Global);
        Application.Quit();
    }

    // Removes pause menu scene & saves settings to settings text file
    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync(1);
        SaveSystem.Save(Global);
    }

    // Opens main menu scene
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}