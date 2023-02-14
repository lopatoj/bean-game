using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [SerializeField] private Global.Global Global;
    
    [SerializeField] private Slider FOVSlider;

    [SerializeField] private TextMeshProUGUI FOVSliderValue;

    [SerializeField] private Slider SensSlider;

    [SerializeField] private TextMeshProUGUI SensSliderValue;

    [SerializeField] private Slider VolumeSlider;

    [SerializeField] private TextMeshProUGUI VolumeSliderValue;

    
    private GameData _game;

    
    private void Start()
    {
        _game = SaveSystem.Load();

        Global.volume = _game.volume;
        Global.fov = _game.fov;
        Global.sensitivity = _game.sensitivity;

        VolumeSlider.value = Global.volume;
        FOVSlider.value = Global.fov;
        SensSlider.value = Global.sensitivity;
    }

    
    private void Update()
    {
        Global.volume = VolumeSlider.value;
        Global.fov = (int) FOVSlider.value;
        Global.sensitivity = SensSlider.value;

        VolumeSliderValue.text = $"{Global.volume:0.0}";
        FOVSliderValue.text = Global.fov + "";
        SensSliderValue.text = $"{Global.sensitivity:0.0}";

        AudioListener.volume = Global.volume;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        SaveSystem.Save(Global);
    }

    public void QuitGame()
    {
        Application.Quit();
        SaveSystem.Save(Global);
    }

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync(1);
        SaveSystem.Save(Global);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}