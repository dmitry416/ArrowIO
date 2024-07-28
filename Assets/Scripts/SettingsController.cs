using UnityEngine.UI;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private GameObject _settings;

    private float _musicValue = 1;
    private float _soundValue = 1;

    private void Start()
    {
        _musicSlider.value = _musicValue;
        _soundSlider.value = _soundValue;
    }

    public void UpdateMusic()
    {
        _musicValue = _musicSlider.value;
    }

    public void UpdateSound()
    {
        _soundValue = _soundSlider.value;
    }


    public void OpenSettings()
    {
        _settings.SetActive(true);
    }

    public void CloseSettings()
    {
        _settings.SetActive(false);
    }

    public void LeftTheGame()
    {
        //scene
    }
}
