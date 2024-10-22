using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private TMP_InputField _nickname; 

    private string _daylyEnd = "";
    private TimeSpan _remain;
    private int _coins;

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

    public void GetLoad()
    {
        openSkins = new int[10];
        for (int i = 0; i < openSkins.Length; ++i)
            openSkins[i] = PlayerPrefs.GetInt(i.ToString(), -1);
        openSkins[4] = 0;
        openWeapons = new bool[6];
        for (int i = 0; i < openWeapons.Length; ++i)
            openWeapons[i] = PlayerPrefs.GetInt("w" + i.ToString(), 0) == 1;
        openWeapons[0] = true;
        curSkin = PlayerPrefs.GetInt("hero", 4);
        curStyle = PlayerPrefs.GetInt("skin", 0);
        curWeapon = PlayerPrefs.GetInt("weapon", 0);
        coins = PlayerPrefs.GetInt("coins", 0);
        _rating.text = PlayerPrefs.GetInt("rating").ToString();
        _daylyEnd = PlayerPrefs.GetString("dayly", "");
        UpdateDalyTimer();
        canGetData?.Invoke();
    }

    public void MySave()
    {
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.Save();
    }

    public void SaveNick()
    {
        PlayerPrefs.SetString("nick", _nickname.text);
        PlayerPrefs.Save();
    }
    public void SaveWeapon()
    {
        for (int i = 0; i < openWeapons.Length; ++i)
            PlayerPrefs.SetInt("w" + i.ToString(), openWeapons[i] ? 1 : 0);
        PlayerPrefs.SetInt("weapon", curWeapon);
        PlayerPrefs.Save();
    }

    public void SkinSave()
    {
        for (int i = 0; i < openSkins.Length; ++i)
            PlayerPrefs.SetInt("w" + i.ToString(), openSkins[i]);
        PlayerPrefs.SetInt("hero", curSkin);
        PlayerPrefs.SetInt("skin", curStyle);
        PlayerPrefs.Save();
    }

    public void SaveDay()
    {
        PlayerPrefs.SetString("dayly", _daylyEnd);
        PlayerPrefs.Save();
    }

    private void Start()
    {
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
            _dayly.text = "Receive";
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
        if (_remain.TotalSeconds <= 1)
        {
            _dayly.text = "Receive";
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
