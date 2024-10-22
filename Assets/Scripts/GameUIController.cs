using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _coinsEarned;
    [SerializeField] private RectTransform _bg;
    [SerializeField] private RectTransform _chest;
    [SerializeField] private RectTransform _button;
    [SerializeField] private RectTransform _buttonAd;
    [SerializeField] private AudioSource _gameAudio;
    [SerializeField] private AudioManager _audioManager;
    private int _totalSeconds = 180;
    private int lvl;
    private int collectedCoins;
    private int rating;

    public Action onTimerEnd;

    public void SetTimer() 
    {
        _timer.gameObject.SetActive(true);
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (_totalSeconds > 0)
        {
            _totalSeconds--;
            _timer.text = $"{_totalSeconds / 60}:{_totalSeconds % 60:00}";
            yield return new WaitForSeconds(1);
        }
        onTimerEnd?.Invoke();
    }

    public void EndPanel(string title)
    {
        TrueGameEnd();
        _gameAudio.Stop();
        _audioManager.PlayEnd();
        
        _title.text = title;
        _coinsEarned.text = $"+{collectedCoins} coins";
        _bg.localScale = _bg.localScale - Vector3.right;
        _title.transform.localScale = Vector3.zero;
        _coinsEarned.transform.localScale = Vector3.zero;
        _chest.transform.localScale = Vector3.zero;
        _button.transform.localScale = Vector3.zero;
        _buttonAd.transform.localScale = Vector3.zero;
        _endPanel.SetActive(true);
        DOTween.Sequence()
            .AppendInterval(2)
            .Append(_endPanel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0.5f), 0.5f))
            .Append(_bg.DOScaleX(1, 0.5f))
            .Append(_title.transform.DOScale(Vector3.one, 0.75f))
            .Append(_coinsEarned.transform.DOScale(Vector3.one, 0.75f))
            .Join(_chest.DOScale(Vector3.one, 0.75f))
            .Append(_button.DOScale(Vector3.one, 0.75f))
            .Join(_buttonAd.DOScale(Vector3.one, 0.75f));
    }

    public void TrueGameEnd()
    {
        lvl = FindObjectOfType<PlayerController>().GetComponent<CharacterController>()._lvl;
        if (lvl <= 2)
            return;
        PlusToSkin();
        collectedCoins = lvl * 50;
        rating = (int)Mathf.Pow(lvl, 2);
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + collectedCoins);
        PlayerPrefs.SetInt("rating", PlayerPrefs.GetInt("rating", 0) + rating);
        PlayerPrefs.Save();
    }

    public void VideoReward()
    {
        collectedCoins *= 2;
        rating *= 2;
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + collectedCoins);
        PlayerPrefs.SetInt("rating", PlayerPrefs.GetInt("rating", 0) + rating);
        PlayerPrefs.Save();
        _coinsEarned.text = $"+{collectedCoins} rating";
    }

    public void PlusToSkin()
    {
        PlayerPrefs.SetInt(PlayerPrefs.GetInt("skin", 0).ToString(), PlayerPrefs.GetInt(PlayerPrefs.GetInt("skin", 0).ToString(), 0) + 1);
        PlayerPrefs.Save();
    }
}
