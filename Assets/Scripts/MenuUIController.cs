using DG.Tweening;
using System;
using System.Collections;
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
        YandexGame.PurchaseSuccessEvent += SuccessPurchased;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
        YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
    }

    void SuccessPurchased(string id)
    {
        print(id);
        switch (id)
        {
            case "100":
                coins += 100;
                break;
            case "500":
                coins += 500;
                break;
            case "1000":
                coins += 1000;
                break;
        }
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

        YandexGame.SaveProgress();
    }

    public void SkinSave()
    {
        YandexGame.savesData.openSkins = openSkins;
        YandexGame.savesData.selectedSkin = curSkin;
        YandexGame.savesData.selectedStyle = curStyle;
        YandexGame.SaveProgress();
    }

    public void SaveNick()
    {
        YandexGame.savesData.nickName = _nickname.text;
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

    public void UpdateDalyTimer()
    {
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
            _coinsUI.text = curCoin.ToString();
            curCoin += step;
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
        UpdateOnlyTimer();
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

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
