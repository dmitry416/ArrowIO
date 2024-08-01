using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using YG;

public enum Lang
{
    ru, uz, kk, be, uk, en, tr, es, de, fr, pt
}

public class LanguageSwapper : MonoBehaviour
{
    [SerializeField] private Image _flag;
    [SerializeField] private Sprite[] _sprites;
    private int _curFlag = 0;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
            GetLoad();
    }

    private void GetLoad()
    {
        switch (YandexGame.EnvironmentData.language)
        {
            case "ru":
                SetFlag((Lang)0);
                break;
            case "uz":
                SetFlag((Lang)1);
                break;
            case "kk":
                SetFlag((Lang)2);
                break;
            case "be":
                SetFlag((Lang)3);
                break;
            case "uk":
                SetFlag((Lang)4);
                break;
            case "en":
                SetFlag((Lang)5);
                break;
            case "tr":
                SetFlag((Lang)6);
                break;
            case "es":
                SetFlag((Lang)7);
                break;
            case "de":
                SetFlag((Lang)8);
                break;
            case "fr":
                SetFlag((Lang)9);
                break;
            case "pt":
                SetFlag((Lang)10);
                break;
        }
    }

    public void SetFlag(Lang flag)
    {
        _flag.sprite = _sprites[(int)flag];
        _curFlag = (int)flag;
        YandexGame.SwitchLanguage(Enum.GetName(typeof(Lang), flag));
        YandexGame.EnvironmentData.language = Enum.GetName(typeof(Lang), flag);
        YandexGame.SaveProgress();
    }

    public void NextFlag()
    {
        SetFlag((Lang)((_curFlag + 1) % _sprites.Length));
    }
}
