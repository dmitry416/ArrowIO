using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour 
{
    private CharacterController _charCont;
    private Hand _hand;
    private PlayerInput _input;
    private SkillGroupController _skillCont;
    private PlayerUIController _playerUI;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Shoot.performed += context => _charCont.Shoot();

        _charCont = GetComponent<CharacterController>();
        _playerUI = GetComponent<PlayerUIController>();
        _hand = GetComponentInChildren<Hand>();
        _skillCont = FindObjectOfType<SkillGroupController>();
        _charCont.onCoinChanged += _playerUI.UpdateUI;
        _charCont.SetNick("Тест");
        _skillCont.onSelect += SelectSkill;
        _charCont.onLVLUp += LVLUp;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        Vector2 dir = _input.Player.Move.ReadValue<Vector2>();
        if (dir == Vector2.zero)
            dir = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        _charCont.Move(dir);
    }

    private void LVLUp()
    {
        _skillCont.Activate();
    }

    private void SelectSkill(Skills skill)
    {
        switch (skill)
        {
            case Skills.Double:
                _hand.isDouble = true;
                break;
            case Skills.FrontSide: 
                _hand.isFrontSide = true;
                break;
            case Skills.Side: 
                _hand.isSide = true;
                break;
            case Skills.Back: 
                _hand.isBack = true;
                break;
            case Skills.Damage:
                _hand._damageBaff += 0.2f;
                break;
            case Skills.CritDamage:
                _hand._critChanceBaff += 0.1f;
                break;
            case Skills.WeaponSpeed:
                _hand._speedBaff += 0.2f;
                break;
            case Skills.Distance:
                _hand._distanceBaff += 0.2f;
                break;
            case Skills.Walkspeed:
                _charCont._speed += 0.5f;
                break;
            case Skills.CD:
                _hand._cd -= 0.1f;
                break;
            case Skills.Health:
                _charCont._health += 25;
                _charCont._curHealth += 25;
                break;
        }
        _charCont.CheckCoins();
    }
}
