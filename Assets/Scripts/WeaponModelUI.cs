using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "1000";
            _buttonIcon.sprite = _coin;
        }
        else if (_curModel == _ui.curWeapon)
        {
            _button.GetComponent<Image>().color = Color.gray;
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "¬€¡–¿ÕŒ";
            _buttonIcon.sprite = _selected;
        }
        else
        {
            _button.GetComponent<Image>().color = Color.yellow;
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "¬€¡–¿“‹";
            _buttonIcon.sprite = _select;
        }
    }

    public void BuyButton()
    {
        if (_button.GetComponent<Image>().color == Color.yellow)
        {
            _ui.curWeapon = _curModel;
        }
        else if (_button.GetComponent<Image>().color == Color.green && _ui.coins >= 1000)
        {
            _ui.coins -= 1000;
            _ui.curWeapon = _curModel;
            _ui.openWeapons[_curModel] = true;
            _ui.MySave();
        }
        else
            return;
        _ui.SaveWeapon();
        _button.GetComponent<Image>().color = Color.gray;
        _button.GetComponentInChildren<TextMeshProUGUI>().text = "¬€¡–¿ÕŒ";
        _buttonIcon.sprite = _selected;
    }
}
