using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _coinsEarned;
    [SerializeField] private RectTransform _bg;
    [SerializeField] private RectTransform _chest;
    [SerializeField] private RectTransform _button;
    private void Start()
    {
        EndPanel("онаедю!!");
    }
    public void EndPanel(string title)
    {
        _title.text = title;
        _bg.localScale = _bg.localScale - Vector3.right;
        _title.transform.localScale = Vector3.zero;
        _coinsEarned.transform.localScale = Vector3.zero;
        _chest.transform.localScale = Vector3.zero;
        _button.transform.localScale = Vector3.zero;
        _endPanel.SetActive(true);
        DOTween.Sequence()
            .Append(_endPanel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0.5f), 0.5f))
            .Append(_bg.DOScaleX(1, 0.5f))
            .Append(_title.transform.DOScale(Vector3.one, 0.75f))
            .Append(_coinsEarned.transform.DOScale(Vector3.one, 0.75f))
            .Join(_chest.DOScale(Vector3.one, 0.75f))
            .Append(_button.DOScale(Vector3.one, 0.75f));
    }
}
