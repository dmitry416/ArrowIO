using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIController : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private TextMeshProUGUI _lvl;

    public void SetLVL(int lvl)
    {
        _lvl.text = lvl.ToString();
    }

    public void SetHP(float hp)
    {
        _hpSlider.value = hp;
    }
}
