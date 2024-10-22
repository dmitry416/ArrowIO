using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
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
    public string description;

    public Skill(Skills name, Sprite sprite, string description)
    {
        this.name = name;
        this.sprite = sprite;
        this.description = description;
    }
}

public class SkillGroupController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private GameObject[] _skillButtons;
    private Image[] _skillButtonsIcons;
    protected Skill[] _skills;
    private Skill[] _activeSkills = new Skill[3];
    [Space]
    [SerializeField] protected Sprite _double;
    [SerializeField] protected Sprite _frontSide;
    [SerializeField] protected Sprite _side;
    [SerializeField] protected Sprite _back;
    [SerializeField] protected Sprite _damage;
    [SerializeField] protected Sprite _critDamage;
    [SerializeField] protected Sprite _weaponSpeed;
    [SerializeField] protected Sprite _distance;
    [SerializeField] protected Sprite _walkSpeed;
    [SerializeField] protected Sprite _cd;
    [SerializeField] protected Sprite _health;
    private AudioManager _audioManager;
    private int _activations = 0;
    private bool _isActive = false;
    public Action<Skills> onSelect;

    private void Awake()
    {
        _skills = new Skill[11];
        _skills[0] = new Skill(Skills.Double, _double, "Two projectiles forward");
        _skills[1] = new Skill(Skills.FrontSide, _frontSide, "Two projectiles on the sides");
        _skills[2] = new Skill(Skills.Side, _side, "Two projectiles on the edges");
        _skills[3] = new Skill(Skills.Back, _back, "One projectile back");
        _skills[4] = new Skill(Skills.Damage, _damage, "Increased damage");
        _skills[5] = new Skill(Skills.CritDamage, _critDamage, "Increased chance of critical damage");
        _skills[6] = new Skill(Skills.WeaponSpeed, _weaponSpeed, "Increased projectile speed");
        _skills[7] = new Skill(Skills.Distance, _distance, "Increased projectile range");
        _skills[8] = new Skill(Skills.Walkspeed, _walkSpeed, "Increased walking speed");
        _skills[9] = new Skill(Skills.CD, _cd, "Reduced recharge time");
        _skills[10] = new Skill(Skills.Health, _health, "Increased health reserve");
        _skillButtonsIcons = new Image[_skillButtons.Length];
        for (int i = 0; i < _skillButtons.Length; ++i)
            _skillButtonsIcons[i] = _skillButtons[i].GetComponentsInChildren<Image>()[1];
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void Activate()
    {
        _audioManager.PlayLVLUp();
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
        _audioManager.PlayButton();
        DOTween.Sequence()
            .Append(_skillButtons[id].GetComponent<RectTransform>().DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f))
            .Append(_skillButtons[2].GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f))
            .Append(_skillButtons[1].GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f))
            .Append(_skillButtons[0].GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f));
        ShowDescription(_activeSkills[id].description);
        onSelect?.Invoke(_activeSkills[id].name);
        _isActive = false;
        _activations--;
        if (_activations > 0)
        {
            _activations--;
            Activate();
        }
    }

    private void ShowDescription(string disc)
    {
        _description.text = disc;
        Invoke("HideDescription", 2);
    }

    private void HideDescription()
    {
        _description.text = "";
    }
}
