using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
using System;

public class ScenLoader : MonoBehaviour
{
    [SerializeField] private Slider _sfxValue;
    [SerializeField] private Slider _musicValue;
    [SerializeField] private TMP_Dropdown _quality;
    [SerializeField] private Slider _joysticSize;
    [SerializeField] private TMP_Dropdown _language;

    [SerializeField] public Action Swap;
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeQuality(int Quality)
    {
        QualitySettings.SetQualityLevel(Quality);
    }

    public void ChangeLanguage(int Lang)
    {
        PlayerPrefs.SetInt("Language", Lang);
        Swap?.Invoke();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Quality", _quality.value);
        PlayerPrefs.SetFloat("Music", _musicValue.value);
        PlayerPrefs.SetFloat("SFX", _sfxValue.value);
        PlayerPrefs.SetFloat("JoysticSize", _joysticSize.value);
    }

    public void LoadSettings()
    {
        _quality.value = PlayerPrefs.GetInt("Quality", 1);
        _musicValue.value = PlayerPrefs.GetFloat("Music", 1f);
        _sfxValue.value = PlayerPrefs.GetFloat("SFX", 1f);
        _joysticSize.value = PlayerPrefs.GetFloat("JoysticSize", 0.5f);
        _language.value = PlayerPrefs.GetInt("Language", 0);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
