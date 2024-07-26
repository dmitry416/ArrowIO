using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Slider _coinSlider;
    [SerializeField] private TextMeshProUGUI _percents;
    [SerializeField] private GameObject _mobilePanelUI;

    public void UpdateUI(int val)
    {
        _coinSlider.value = val;
        _percents.text = $"{val}%";
    }

    public void SetMobileUI()
    {
        _mobilePanelUI.SetActive(true);
    }
}
