using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject _main;
    [Space]
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _game1;
    [SerializeField] private GameObject _game2;
    [Space]
    [SerializeField] private GameObject _donatPanel;
    [SerializeField] private GameObject _donat1;
    [SerializeField] private GameObject _donat2;
    [SerializeField] private GameObject _donat3;
    [Space]
    [SerializeField] private GameObject _settings;
    [SerializeField] private TextMeshProUGUI _coinsUI;
    [SerializeField] private TextMeshProUGUI _dayly;
    [SerializeField] private TextMeshProUGUI _rating;
    [SerializeField] private Button _daylyButton;

    private string _daylyEnd = "";
    private TimeSpan _remain;
    private int _coins;
    private Dictionary<string, string> _collectText = new Dictionary<string, string> { { "ru", "Получить" }, { "uz", "Oling" }, { "kk", "Алу" }, { "be", "Атрымаць" }, { "uk", "Одержавши" }, { "en", "Receive" }, { "tr", "Almak" }, { "es", "Obtener" }, { "de", "Bekommen" }, { "fr", "Recevoir" }, { "pt", "Obter" } };

    public Action canGetData;
    [HideInInspector] public int curSkin;
    [HideInInspector] public int curStyle;
    [HideInInspector] public int curWeapon;
    [HideInInspector] public int[] openSkins;
    [HideInInspector] public bool[] openWeapons;
    public int coins 
    {
        get { return _coins; }
        set { _coins = value; UpdateCoins(); }
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
        YandexGame.PurchaseSuccessEvent += SuccessPurchased;
        YandexGame.SwitchLangEvent += UpdateDalyTimer;
        YandexGame.SwitchLangEvent += UpdateOnlyTimer;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
        YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
        YandexGame.SwitchLangEvent -= UpdateDalyTimer;
        YandexGame.SwitchLangEvent -= UpdateOnlyTimer;
    }

    void SuccessPurchased(string id)
    {
        switch (id)
        {
            case "2000":
                coins += 2000;
                break;
            case "500":
                coins += 500;
                break;
            case "1000":
                coins += 1000;
                break;
        }
        MySave();
    }

    public void GetLoad()
    {
        YandexGame.GameplayStop();
        openSkins = YandexGame.savesData.openSkins;
        openWeapons = YandexGame.savesData.openWeapons;
        curSkin = YandexGame.savesData.selectedSkin;
        curStyle = YandexGame.savesData.selectedStyle;
        curWeapon = YandexGame.savesData.selectedWeapon;
        coins = YandexGame.savesData.coins;
        _rating.text = YandexGame.savesData.rating.ToString();
        _daylyEnd = YandexGame.savesData.daylyEnded;
        UpdateDalyTimer(YandexGame.savesData.language);
        canGetData?.Invoke();
    }

    public void MySave()
    {
        YandexGame.savesData.coins = coins;
        YandexGame.SaveProgress();
    }

    public void SaveWeapon()
    {
        YandexGame.savesData.openWeapons = openWeapons;
        YandexGame.savesData.selectedWeapon = curWeapon;
        YandexGame.SaveProgress();
    }

    public void SkinSave()
    {
        YandexGame.savesData.openSkins = openSkins;
        YandexGame.savesData.selectedSkin = curSkin;
        YandexGame.savesData.selectedStyle = curStyle;
        YandexGame.SaveProgress();
    }

    public void SaveDay()
    {
        YandexGame.savesData.daylyEnded = _daylyEnd;
        YandexGame.SaveProgress();
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
            GetLoad();
    }

    public void OpenGamePanel()
    {
        _game1.transform.localScale = Vector3.zero;
        _game2.transform.localScale = Vector3.zero;
        _gamePanel.transform.localScale = Vector3.one - Vector3.right;

        _gamePanel.SetActive(true);
        DOTween.Sequence()
            .Append(_gamePanel.transform.DOScale(Vector3.one, 0.5f))
            .Append(_game1.transform.DOScale(Vector3.one * 0.6f, 0.5f))
            .Append(_game2.transform.DOScale(Vector3.one * 0.6f, 0.5f));
    }

    public void OpenDonatPanel()
    {
        _donat1.transform.localScale = Vector3.zero;
        _donat2.transform.localScale = Vector3.zero;
        _donat3.transform.localScale = Vector3.zero;
        _donatPanel.transform.localScale = Vector3.one - Vector3.right;

        _donatPanel.SetActive(true);
        DOTween.Sequence()
            .Append(_donatPanel.transform.DOScale(Vector3.one, 0.5f))
            .Append(_donat1.transform.DOScale(Vector3.one * 1.5f, 0.5f))
            .Append(_donat2.transform.DOScale(Vector3.one * 1.5f, 0.5f))
            .Append(_donat3.transform.DOScale(Vector3.one * 1.5f, 0.5f));
    }

    public void UpdateDalyTimer(string lang)
    {
        if (_daylyEnd == "")
        {
            _dayly.text = _collectText[lang];
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
        if (_coinsUI.text == "")
            _coinsUI.text = coins.ToString();
        else
            StartCoroutine(CoinRoll());
        MySave();
    }

    private IEnumerator CoinRoll()
    {
        int curCoin = Convert.ToInt32(_coinsUI.text);
        int step = curCoin < coins ? 1 : -1;
        while (curCoin != coins)
        {
            for (int i = 0; i < 5 && curCoin != coins; ++i)
                curCoin += step;    
            _coinsUI.text = curCoin.ToString();
            yield return null;
        }
    }

    public void CollectDayly()
    {
        _daylyEnd = (DateTime.Now + new TimeSpan(1, 0, 0)).ToString("F");
        _daylyButton.interactable = false;
        _remain = DateTime.Parse(_daylyEnd) - DateTime.Now;
        SaveDay();
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
        UpdateOnlyTimer(YandexGame.EnvironmentData.language);
        TimeSpan second = new TimeSpan(0, 0, 1);
        while (_remain.TotalSeconds > 0)
        {
            UpdateOnlyTimer(YandexGame.EnvironmentData.language);
            yield return new WaitForSeconds(1);
            _remain -= second;
        }
    }

    private void UpdateOnlyTimer(string lang)
    {
        if (_remain.TotalSeconds <= 1)
        {
            _dayly.text = _collectText[lang];
            _daylyButton.interactable = true;
        }
        else
        {
            _dayly.text = $"{_remain.Minutes}:{_remain.Seconds:00}";
        }
    }

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
