using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponModelUI : MonoBehaviour
{
    public GameObject[] _weaponModels;
    [SerializeField] private MenuUIController _ui;
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _buttonIcon;
    [SerializeField] private Sprite _coin;
    [SerializeField] private Sprite _select;
    [SerializeField] private Sprite _selected;
    private int _curModel;

    private Dictionary<string, string> _selectText = new Dictionary<string, string> { { "ru", "ВЫБРАТЬ" }, { "uz", "Tanlang" }, { "kk", "Таңдау" }, { "be", "ВЫБРАЦЬ" }, { "uk", "ВИБРАТИ" }, { "en", "SELECT" }, { "tr", "SEÇMEK" }, { "es", "ELEGIR" }, { "de", "wählen" }, { "fr", "CHOISIR" }, { "pt", "SELECIONAR" } };
    private Dictionary<string, string> _selectedText = new Dictionary<string, string> { { "ru", "ВЫБРАНО" }, { "uz", "Tanlangan" }, { "kk", "Таңдалған" }, { "be", "ВЫБРАНЫ" }, { "uk", "ВИБРАНИЙ" }, { "en", "SELECTED" }, { "tr", "SEÇME" }, { "es", "SELECCIONADO" }, { "de", "AKTIVIERT" }, { "fr", "SÉLECTIONNÉ" }, { "pt", "PREFERIDO" } };

    public void Despawn()
    {
        if (transform.childCount != 0)
            Destroy(transform.GetChild(0).gameObject);
    }

    public void SetActiveModel()
    {
        _curModel = _ui.curWeapon;
        SetModel();
    }

    public void SetNextModel()
    {
        _curModel = (_curModel + 1) % 6;
        SetModel();
    }

    public void SetPreviousModel()
    {
        _curModel = (6 + _curModel - 1) % 6;
        SetModel();
    }

    public void SetModel()
    {
        Despawn();
        Instantiate(_weaponModels[_curModel], transform);
        if (!_ui.openWeapons[_curModel])
        {
            _button.GetComponent<Image>().color = Color.green;
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "5000";
            _buttonIcon.sprite = _coin;
        }
        else if (_curModel == _ui.curWeapon)
        {
            _button.GetComponent<Image>().color = Color.gray;
            _button.GetComponentInChildren<TextMeshProUGUI>().text = _selectedText[YandexGame.EnvironmentData.language];
            _buttonIcon.sprite = _selected;
        }
        else
        {
            _button.GetComponent<Image>().color = Color.yellow;
            _button.GetComponentInChildren<TextMeshProUGUI>().text = _selectText[YandexGame.EnvironmentData.language];
            _buttonIcon.sprite = _select;
        }
    }

    public void BuyButton()
    {
        if (_button.GetComponent<Image>().color == Color.yellow)
        {
            _ui.curWeapon = _curModel;
        }
        else if (_button.GetComponent<Image>().color == Color.green && _ui.coins >= 5000)
        {
            _ui.coins -= 5000;
            _ui.curWeapon = _curModel;
            _ui.openWeapons[_curModel] = true;
            _ui.MySave();
        }
        else
            return;
        _ui.SaveWeapon();
        _button.GetComponent<Image>().color = Color.gray;
        _button.GetComponentInChildren<TextMeshProUGUI>().text = _selectedText[YandexGame.EnvironmentData.language];
        _buttonIcon.sprite = _selected;
    }
}
