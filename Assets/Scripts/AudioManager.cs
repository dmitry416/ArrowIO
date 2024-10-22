using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _button;
    [SerializeField] private AudioClip _coins;
    [SerializeField] private AudioClip _end;
    [SerializeField] private AudioClip _lvlup;
    [SerializeField] private AudioSource _music;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        FindObjectOfType<SettingsController>().onUpdate += UpdateVolume;
        UpdateVolume();
    }

    public void PlayButton()
    {
        _audioSource.clip = _button;
        _audioSource.Play();
    }

    public void PlayCoinsDelayed(float delay)
    {
        Invoke("PlayCoins", delay);
    }

    public void PlayCoins()
    {
        _audioSource.clip = _coins;
        _audioSource.Play();
    }

    public void PlayEnd()
    {
        _audioSource.clip = _end;
        _audioSource.Play();
    }

    public void PlayLVLUp()
    {
        _audioSource.clip = _lvlup;
        _audioSource.Play();
    }

    private void UpdateVolume()
    {
        _audioSource.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        _music.volume = PlayerPrefs.GetFloat("music", 0.5f);
    }
}
