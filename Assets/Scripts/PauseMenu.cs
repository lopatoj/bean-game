using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Global Global;

    public Slider VolumeSlider;
    public Slider FOVSlider;
    public Slider SensSlider;

    public TextMeshProUGUI VolumeSliderValue;
    public TextMeshProUGUI FOVSliderValue;
    public TextMeshProUGUI SensSliderValue;

    void Start()
    {
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

    public void Resume()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(1, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
    }

    public void Quit()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
