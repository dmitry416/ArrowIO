using UnityEngine.UI;
using UnityEngine;
using YG;
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

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    public void GetLoad()
    {
        musicValue = YandexGame.savesData.musicValue;
        soundValue = YandexGame.savesData.soundValue;
        _musicSlider.value = musicValue;
        _soundSlider.value = soundValue;
    }

    public void MySave()
    {
        YandexGame.savesData.musicValue = musicValue;
        YandexGame.savesData.soundValue = soundValue;

        YandexGame.SaveProgress();
        onUpdate?.Invoke();
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
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
