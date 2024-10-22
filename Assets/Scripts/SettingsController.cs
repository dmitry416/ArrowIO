using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private GameObject _settings;

    public float musicValue = 1;
    public float soundValue = 1;
    public Action onUpdate;

    public void GetLoad()
    {
        musicValue = PlayerPrefs.GetFloat("music", 0.5f);
        soundValue = PlayerPrefs.GetFloat("sound", 0.5f);
        _musicSlider.value = musicValue;
        _soundSlider.value = soundValue;
    }

    public void MySave()
    {
        PlayerPrefs.SetFloat("music", musicValue);
        PlayerPrefs.SetFloat("sound", soundValue);
        PlayerPrefs.Save();
        onUpdate?.Invoke();
    }

    private void Start()
    {
        GetLoad();
    }

    public void UpdateMusic()
    {
        musicValue = _musicSlider.value;
    }

    public void UpdateSound()
    {
        soundValue = _soundSlider.value;
    }


    public void OpenSettings()
    {
        _settings.SetActive(true);
    }

    public void CloseSettings()
    {
        _settings.SetActive(false);
        MySave();
    }

    public void LeftTheGame()
    {
        SceneManager.LoadScene(0);
    }
}
