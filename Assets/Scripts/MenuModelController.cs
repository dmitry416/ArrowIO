using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuModelController : MonoBehaviour
{
    public GameObject[] _characterModels;
    [SerializeField] private MenuUIController _ui;
    [SerializeField] private ModelSliderUI _modelSlider;
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _buttonIcon;
    [SerializeField] private Sprite _coin;
    [SerializeField] private Sprite _select;
    [SerializeField] private Sprite _selected;
    private int _curModel;

    public void SetActiveModel()
    {
        _curModel = _ui.curSkin;
        SetModel();
    }

    public void SetNextModel()
    {
        _curModel = (_curModel + 1) % 10;
        SetModel();
    }

    public void SetPreviousModel()
    {
        _curModel = (10 + _curModel - 1) % 10;
        SetModel();
    }

    public void SetModel()
    {
        if (transform.childCount != 0)
            Destroy(transform.GetChild(0).gameObject);
        GameObject model = Instantiate(_characterModels[_curModel], transform);
        model.GetComponent<Animator>().SetTrigger("win");
        if (_curModel == _ui.curSkin)
            model.GetComponent<CharacterModel>().ChangeSkin(_ui.curStyle);
        else
            model.GetComponent<CharacterModel>().ChangeSkin(_ui.openSkins[_curModel] == -1 ? 0 : _ui.openSkins[_curModel] / 10);
        _modelSlider.SetSlider(Mathf.Min(_ui.openSkins[_curModel], 40));
        _modelSlider.SetSelected(_ui.openSkins[_curModel] == -1 ? 0 : _ui.openSkins[_curModel] / 10);
        if (_ui.openSkins[_curModel] == -1)
        {
            _button.GetComponent<Image>().color = Color.green;
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "1000";
            _buttonIcon.sprite = _coin;
        }
        else if (_curModel == _ui.curSkin)
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
            _ui.curSkin = _curModel;
            _ui.curStyle = _modelSlider.selectedID;
        }
        else if (_button.GetComponent<Image>().color == Color.green && _ui.coins >= 1000)
        {
            _ui.coins -= 1000;
            _ui.curSkin = _curModel;
            _ui.curStyle = 0;
            _ui.openSkins[_ui.curSkin]++;
            _ui.MySave();
        }
        else
            return;
        _ui.SkinSave();
        _button.GetComponent<Image>().color = Color.gray;
        _button.GetComponentInChildren<TextMeshProUGUI>().text = "¬€¡–¿ÕŒ";
        _buttonIcon.sprite = _selected;
    }

    private void Awake()
    {
        _ui.canGetData += SetActiveModel;
    }
}
