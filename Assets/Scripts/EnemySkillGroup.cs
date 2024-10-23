using UnityEngine;

public class EnemySkillGroup : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private CharacterControllerMy _charCont;

    public void SelectSkill(Skills skill)
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

    public Skills RandomSkill()
    {
        return (Skills)Random.Range(0, 11);
    }
}
