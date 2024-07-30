using UnityEngine;
using UnityEngine.UI;

public class ModelSliderUI : MonoBehaviour
{
    [SerializeField] private Image[] _checkers;
    [SerializeField] private Sprite _enable;
    [SerializeField] private Sprite _disable;
    [SerializeField] private Sprite _selected;
    [SerializeField] private Slider _slider;

    public int selectedID = 0;

    public void SetSlider(int value)
    {
        foreach (Image checker in _checkers)
        {
            checker.sprite = _disable;
            checker.GetComponent<Button>().interactable = false;
        }
        _slider.value = value;
        for (int i = 0, j = 0; j <= value && i < _checkers.Length; ++i, j += 10)
        {
            _checkers[i].sprite = _enable;
            _checkers[i].GetComponent<Button>().interactable = true;
        }
    }

    public void SetSelected(int id)
    {
        _checkers[id].sprite = _enable;
        selectedID = id;
    }
}
