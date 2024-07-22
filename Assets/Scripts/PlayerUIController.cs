using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Slider _coinSlider;
    [SerializeField] private TextMeshProUGUI _percents;

    public void UpdateUI(int val)
    {
        print(val);
        _coinSlider.value = val;
        _percents.text = $"{val}%";
    }
}
