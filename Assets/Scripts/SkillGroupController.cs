using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

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

    private Dictionary<string, string> _doubleText = new Dictionary<string, string> { { "ru", "Два снаряда вперед" }, { "uz", "Oldinga ikkita snaryad" }, { "kk", "Екі снаряд Алға" }, { "be", "Два снарады наперад" }, { "uk", "Два снаряди вперед" }, { "en", "Two projectiles forward" }, { "tr", "İki mermi ileri" }, { "es", "Dos proyectiles hacia adelante" }, { "de", "Zwei Projektile nach vorne" }, { "fr", "Deux projectiles en avant" }, { "pt", "Dois projéteis para a frente" } };
    private Dictionary<string, string> _frontSideText = new Dictionary<string, string> { { "ru", "Два снаряда по бокам" }, { "uz", "yon tomonlardagi ikkita snaryad" }, { "kk", "екі жағынан екі снаряд" }, { "be", "Два снарады па баках" }, { "uk", "Два снаряди з боків" }, { "en", "Two projectiles on the sides" }, { "tr", "Yanlarda iki mermi" }, { "es", "dos proyectiles a los lados" }, { "de", "Zwei Projektile an den Seiten" }, { "fr", "deux projectiles sur les côtés" }, { "pt", "dois projéteis para os lados" } };
    private Dictionary<string, string> _sideText = new Dictionary<string, string> { { "ru", "Два снаряда по краям" }, { "uz", "Chetidagi ikkita snaryad" }, { "kk", "Шеттеріндегі екі снаряд" }, { "be", "Два снарады па краях" }, { "uk", "Два снаряди по краях" }, { "en", "Two projectiles on the edges" }, { "tr", "Kenarlarda iki mermi" }, { "es", "Dos proyectiles en los bordes" }, { "de", "Zwei Projektile an den Rändern" }, { "fr", "Deux projectiles sur les bords" }, { "pt", "Dois projéteis nas bordas" } };
    private Dictionary<string, string> _backText = new Dictionary<string, string> { { "ru", "Один снаряд назад" }, { "uz", "bitta snaryad orqaga" }, { "kk", "бір снаряд артқа" }, { "be", "адзін снарад назад" }, { "uk", "один снаряд назад" }, { "en", "One projectile back" }, { "tr", "Bir mermi geri" }, { "es", "un proyectil hacia atrás" }, { "de", "Ein Projektil zurück" }, { "fr", "un projectile en arrière" }, { "pt", "um projétil para trás" } };
    private Dictionary<string, string> _damageText = new Dictionary<string, string> { { "ru", "Увеличен урон" }, { "uz", "Zarar ko'paytirildi" }, { "kk", "Зақымдану ұлғайтылды" }, { "be", "Павялічаны страты" }, { "uk", "Збільшено шкоду" }, { "en", "Increased damage" }, { "tr", "Hasar arttı" }, { "es", "Mayor daño" }, { "de", "Erhöhter Schaden" }, { "fr", "Augmente les dégâts" }, { "pt", "Dano aumentado" } };
    private Dictionary<string, string> _critDamageText = new Dictionary<string, string> { { "ru", "Увеличен шанс критического урона" }, { "uz", "tanqidiy shikastlanish ehtimoli oshdi" }, { "kk", "сыни зақымдану мүмкіндігі артты" }, { "be", "павялічаны шанец крытычнага ўрону" }, { "uk", "збільшено шанс критичної шкоди" }, { "en", "Increased chance of critical damage" }, { "tr", "Kritik hasar şansı arttı" }, { "es", "mayor probabilidad de daño crítico" }, { "de", "Die Chance auf kritischen Schaden wurde erhöht" }, { "fr", "augmente les chances de dégâts critiques" }, { "pt", "chance de Dano Crítico aumentada" } };
    private Dictionary<string, string> _weaponSpeedText = new Dictionary<string, string> { { "ru", "Увеличена скорость снаряда" }, { "uz", "Snaryad tezligi oshirildi" }, { "kk", "Снарядтың жылдамдығы артты" }, { "be", "Павялічана хуткасць снарада" }, { "uk", "Збільшена швидкість снаряда" }, { "en", "Increased projectile speed" }, { "tr", "Merminin hızı arttı" }, { "es", "Aumento de la velocidad del proyectil" }, { "de", "Geschossgeschwindigkeit erhöht" }, { "fr", "Augmentation de la vitesse du projectile" }, { "pt", "Maior velocidade do projétil" } };
    private Dictionary<string, string> _distanceText = new Dictionary<string, string> { { "ru", "Увеличена дальность снаряда" }, { "uz", "snaryad masofasi oshirildi" }, { "kk", "снарядтың ауқымы артты" }, { "be", "павялічана далёкасць снарада" }, { "uk", "збільшена дальність снаряда" }, { "en", "Increased projectile range" }, { "tr", "Merminin menzili arttı" }, { "es", "Aumento del alcance del proyectil" }, { "de", "Geschossreichweite erhöht" }, { "fr", "augmentation de la portée du projectile" }, { "pt", "maior alcance do projétil" } };
    private Dictionary<string, string> _walkspeedText = new Dictionary<string, string> { { "ru", "Увеличена скорость ходьбы" }, { "uz", "Yurish tezligini oshirdi" }, { "kk", "Жүру жылдамдығы артты" }, { "be", "Павялічана хуткасць хады" }, { "uk", "Збільшена швидкість ходьби" }, { "en", "Increased walking speed" }, { "tr", "Yürüme hızı arttı" }, { "es", "Mayor velocidad de marcha" }, { "de", "Erhöhte Gehgeschwindigkeit" }, { "fr", "Vitesse de marche accrue" }, { "pt", "Velocidade de caminhada aumentada" } };
    private Dictionary<string, string> _cdText = new Dictionary<string, string> { { "ru", "Уменьшено время перезарядки" }, { "uz", "zaryadlash vaqtini qisqartirdi" }, { "kk", "қайта зарядтау уақыты азайды" }, { "be", "зменшана час перазарадкі" }, { "uk", "зменшено час перезарядки" }, { "en", "Reduced recharge time" }, { "tr", "Yeniden yükleme süresi azaldı" }, { "es", "menor tiempo de recarga" }, { "de", "Abklingzeit wurde verringert" }, { "fr", "temps de recharge Réduit" }, { "pt", "tempo de recarga reduzido" } };
    private Dictionary<string, string> _healthText = new Dictionary<string, string> { { "ru", "Увеличен запас здоровья" }, { "uz", "Sog'liqni saqlash zaxirasi oshirildi" }, { "kk", "Денсаулық сақтау қоры артты" }, { "be", "Павялічаны запас здароўя" }, { "uk", "Збільшено запас здоров'я" }, { "en", "Increased health reserve" }, { "tr", "Artan sağlık rezervi" }, { "es", "Aumento de la salud" }, { "de", "Erhöhte Gesundheit" }, { "fr", "Augmentation de la réserve de santé" }, { "pt", "Vida aumentada" } };

    private void Awake()
    {
        _skills = new Skill[11];
        _skills[0] = new Skill(Skills.Double, _double, _doubleText[YandexGame.EnvironmentData.language]);
        _skills[1] = new Skill(Skills.FrontSide, _frontSide, _frontSideText[YandexGame.EnvironmentData.language]);
        _skills[2] = new Skill(Skills.Side, _side, _sideText[YandexGame.EnvironmentData.language]);
        _skills[3] = new Skill(Skills.Back, _back, _backText[YandexGame.EnvironmentData.language]);
        _skills[4] = new Skill(Skills.Damage, _damage, _damageText[YandexGame.EnvironmentData.language]);
        _skills[5] = new Skill(Skills.CritDamage, _critDamage, _critDamageText[YandexGame.EnvironmentData.language]);
        _skills[6] = new Skill(Skills.WeaponSpeed, _weaponSpeed, _weaponSpeedText[YandexGame.EnvironmentData.language]);
        _skills[7] = new Skill(Skills.Distance, _distance, _distanceText[YandexGame.EnvironmentData.language]);
        _skills[8] = new Skill(Skills.Walkspeed, _walkSpeed, _walkspeedText[YandexGame.EnvironmentData.language]);
        _skills[9] = new Skill(Skills.CD, _cd, _cdText[YandexGame.EnvironmentData.language]);
        _skills[10] = new Skill(Skills.Health, _health, _healthText[YandexGame.EnvironmentData.language]);
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
