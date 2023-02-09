using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Global Global;

    [SerializeField]
    private Slider VolumeSlider;
    [SerializeField]
    private Slider FOVSlider;
    [SerializeField]
    private Slider SensSlider;

    [SerializeField]
    private TextMeshProUGUI VolumeSliderValue;
    [SerializeField]
    private TextMeshProUGUI FOVSliderValue;
    [SerializeField]
    private TextMeshProUGUI SensSliderValue;

    void Start()
    {
        //Global = SaveSystem.LoadGlobals();

        VolumeSlider.value = Global.volume;
        FOVSlider.value = Global.fov;
        SensSlider.value = Global.sensitivity;
    }

    void Update()
    {
        Global.volume = VolumeSlider.value;
        Global.fov = (int)FOVSlider.value;
        Global.sensitivity = SensSlider.value;

        VolumeSliderValue.text = string.Format("{0:0.0}", Global.volume);
        FOVSliderValue.text = Global.fov + "";
        SensSliderValue.text = string.Format("{0:0.0}", Global.sensitivity);

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