using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

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

    private Dictionary<string, string> _coinsText = new Dictionary<string, string> { { "ru", "монет" }, { "uz", "tangalar" }, { "kk", "монеталар" }, { "be", "манета" }, { "uk", "монета" }, { "en", "coins" }, { "tr", "paralar" }, { "es", "metálico" }, { "de", "Münzen" }, { "fr", "espèce" }, { "pt", "SELECIONAR" } };
    private Dictionary<string, string> _ratingText = new Dictionary<string, string> { { "ru", "рейтинг" }, { "uz", "reyting" }, { "kk", "рейтинг" }, { "be", "рэйтынг" }, { "uk", "рейтинг" }, { "en", "rating" }, { "tr", "derecelendirme" }, { "es", "rating" }, { "de", "Bewertung" }, { "fr", "classement" }, { "pt", "classificação" } };


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
        _gameAudio.Stop();
        _audioManager.PlayEnd();
        PlusToSkin();
        _title.text = title;
        lvl = FindObjectOfType<PlayerController>().GetComponent<CharacterController>()._lvl;
        collectedCoins = lvl > 2 ? lvl * 50 : 0;
        rating = lvl > 2 ? (int)Mathf.Pow(lvl, 2) : 0;
        YandexGame.savesData.coins += collectedCoins;
        YandexGame.savesData.rating += rating;
        YandexGame.SaveProgress();
        YandexGame.NewLeaderboardScores("rating", YandexGame.savesData.rating);
        _coinsEarned.text = $"+{collectedCoins} {_coinsText[YandexGame.EnvironmentData.language]}\n+{rating} {_ratingText[YandexGame.EnvironmentData.language]}";
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

    public void VideoReward()
    {
        collectedCoins *= 2;
        rating *= 2;
        YandexGame.savesData.coins += collectedCoins;
        YandexGame.savesData.rating += rating;
        YandexGame.SaveProgress();
        YandexGame.NewLeaderboardScores("rating", YandexGame.savesData.rating);
        _coinsEarned.text = $"+{collectedCoins} {_coinsText[YandexGame.EnvironmentData.language]}\n+{rating} {_ratingText[YandexGame.EnvironmentData.language]}";
    }

    public void PlusToSkin()
    {
        YandexGame.savesData.openSkins[YandexGame.savesData.selectedSkin]++;
        YandexGame.SaveProgress();
    }
}
