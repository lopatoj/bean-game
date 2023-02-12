using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Slider FOVSlider;

    [SerializeField] private TextMeshProUGUI FOVSliderValue;

    [SerializeField] private Global Global;

    [SerializeField] private Slider SensSlider;

    [SerializeField] private TextMeshProUGUI SensSliderValue;

    [SerializeField] private Slider VolumeSlider;

    [SerializeField] private TextMeshProUGUI VolumeSliderValue;

    private void Start()
    {
        //Global = SaveSystem.LoadGlobals();

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
        //SaveSystem.SaveGlobals(Global);
    }

    public void QuitGame()
    {
        Application.Quit();
        //SaveSystem.SaveGlobals(Global);
    }

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync(1);
        //SaveSystem.SaveGlobals(Global);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}