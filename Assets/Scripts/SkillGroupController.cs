using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum Skills
{
    Double, FrontSide, Side, Back, Damage, CritDamage, WeaponSpeed, Distance, Walkspeed, CD, Health
}

public struct Skill
{
    public Skills name;
    public Sprite sprite;

    public Skill(Skills name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }
}

public class SkillGroupController : MonoBehaviour
{
    [SerializeField] private GameObject[] _skillButtons;
    private Image[] _skillButtonsIcons;
    private Skill[] _skills;
    private Skill[] _activeSkills = new Skill[3];
    [Space]
    [SerializeField] private Sprite _double;
    [SerializeField] private Sprite _frontSide;
    [SerializeField] private Sprite _side;
    [SerializeField] private Sprite _back;
    [SerializeField] private Sprite _damage;
    [SerializeField] private Sprite _critDamage;
    [SerializeField] private Sprite _weaponSpeed;
    [SerializeField] private Sprite _distance;
    [SerializeField] private Sprite _walkSpeed;
    [SerializeField] private Sprite _cd;
    [SerializeField] private Sprite _health;
    private int _activations = 0;
    private bool _isActive = false;
    public Action<Skills> onSelect; 

    private void Awake()
    {
        _skills = new Skill[11];
        _skills[0] = new Skill(Skills.Double, _double);
        _skills[1] = new Skill(Skills.FrontSide, _frontSide);
        _skills[2] = new Skill(Skills.Side, _side);
        _skills[3] = new Skill(Skills.Back, _back);
        _skills[4] = new Skill(Skills.Damage, _damage);
        _skills[5] = new Skill(Skills.CritDamage, _critDamage);
        _skills[6] = new Skill(Skills.WeaponSpeed, _weaponSpeed);
        _skills[7] = new Skill(Skills.Distance, _distance);
        _skills[8] = new Skill(Skills.Walkspeed, _walkSpeed);
        _skills[9] = new Skill(Skills.CD, _cd);
        _skills[10] = new Skill(Skills.Health, _health);
        _skillButtonsIcons = new Image[_skillButtons.Length];
        for (int i = 0; i < _skillButtons.Length; ++i)
            _skillButtonsIcons[i] = _skillButtons[i].GetComponentsInChildren<Image>()[1];
    }

    public void Activate()
    {
        _activations++;
        if (_isActive)
            return;
        _isActive = true;
        foreach (GameObject skill in _skillButtons)
            skill.GetComponent<Button>().interactable = true;
        for (int i = 0; i < _skillButtonsIcons.Length; ++i) 
        {
            int id = UnityEngine.Random.Range(0, _skills.Length);
            _skillButtonsIcons[i].sprite = _skills[id].sprite;
            _skillButtonsIcons[i].GetComponentsInParent<RectTransform>()[1].localScale = Vector3.zero;
            _activeSkills[i] = _skills[id];

        }
        DOTween.Sequence().AppendInterval(0.75f)
            .Append(_skillButtons[0].GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f))
            .Append(_skillButtons[1].GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f))
            .Append(_skillButtons[2].GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
        
    }
    public void Deactivate()
    {
        foreach (GameObject skill in _skillButtons)
            skill.GetComponent<Button>().interactable = false;
    }

    public void OnClickButton(int id)
    {
        DOTween.Sequence()
            .Append(_skillButtons[id].GetComponent<RectTransform>().DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f))
            .Append(_skillButtons[2].GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f))
            .Append(_skillButtons[1].GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f))
            .Append(_skillButtons[0].GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f));
        onSelect?.Invoke(_activeSkills[id].name);
        _isActive = false;
        _activations--;
        if (_activations > 0)
        {
            _activations--;
            Activate();
        }
    }
}
