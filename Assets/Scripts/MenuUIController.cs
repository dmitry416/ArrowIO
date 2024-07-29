using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject _main;
    [SerializeField] private GameObject _settings;
    [SerializeField] private TextMeshProUGUI _coinsUI;
    [SerializeField] private TMP_InputField _nickname;
    [SerializeField] private TextMeshProUGUI _dayly;
    [SerializeField] private TextMeshProUGUI _rating;
    [SerializeField] private Button _daylyButton;

    private string _daylyEnd = "";
    private TimeSpan _remain;
    private int _coins;

    public Action canGetData;
    [HideInInspector] public int curSkin;
    [HideInInspector] public int curStyle;
    [HideInInspector] public int[] openSkins;
    public int coins 
    {
        get { return _coins; }
        set { _coins = value; UpdateCoins(); }
    }

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
        openSkins = YandexGame.savesData.openSkins;
        curSkin = YandexGame.savesData.selectedSkin;
        curStyle = YandexGame.savesData.selectedStyle;
        coins = YandexGame.savesData.coins;
        _nickname.text = YandexGame.savesData.nickName;
        _rating.text = YandexGame.savesData.rating.ToString();
        _daylyEnd = YandexGame.savesData.daylyEnded;
        UpdateCoins();
        UpdateDalyTimer();
        canGetData?.Invoke();
    }

    public void MySave()
    {
        YandexGame.savesData.coins = coins;
        YandexGame.savesData.nickName = _nickname.text;
        YandexGame.savesData.daylyEnded = _daylyEnd;

        YandexGame.SaveProgress();
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
            GetLoad();
    }

    public void UpdateDalyTimer()
    {
        print(_daylyEnd);
        if (_daylyEnd == "")
        {
            _dayly.text = "забрать";
            _daylyButton.interactable = true;
        }
        else
        {
            _remain = DateTime.Parse(_daylyEnd) - DateTime.Now;
            StartCoroutine(Counter());
        }
    }

    public void UpdateCoins()
    {
        _coinsUI.text = coins.ToString();
        MySave();
    }

    public void CollectDayly()
    {
        //Reward
        _daylyEnd = (DateTime.Now + new TimeSpan(1, 0, 0)).ToString("F");
        _daylyButton.interactable = false;
        _remain = DateTime.Parse(_daylyEnd) - DateTime.Now;
        MySave();
        StartCoroutine(Counter());
    }

    public void OpenSettings()
    {
        _settings.transform.DOScaleY(0.9f, 0.25f);
        _settings.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0.5f), 0.5f);
    }

    public void CloseSettings()
    {
        _settings.transform.DOScaleY(0f, 0.25f);
        _settings.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
    }

    private IEnumerator Counter()
    {
        TimeSpan second = new TimeSpan(0, 0, 1);
        while (_remain.TotalSeconds > 0)
        {
            UpdateOnlyTimer();
            yield return new WaitForSeconds(1);
            _remain -= second;
        }
    }

    private void UpdateOnlyTimer()
    {
        if (_remain.TotalSeconds <= 0)
        {
            _dayly.text = "забрать";
            _daylyButton.interactable = true;
        }
        else
        {
            _dayly.text = _remain.Minutes.ToString() + ":" + _remain.Seconds.ToString();
        }
    }
}
