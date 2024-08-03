using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private MenuUIController _ui;
    [SerializeField] private TextMeshProUGUI _coins;
    [SerializeField] private Transform _gift;
    [SerializeField] private Transform _bg;
    private int _reward;


    private void OnEnable()
    {
        Invoke("Active", 3);
        _reward = Random.Range(100, 500);
        _coins.text = _reward.ToString();
        _bg.DORotate(new Vector3(0, 0, -90), 1f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        DOTween.Sequence()
            .Append(_gift.DOShakeScale(3, 0.2f))
            .Append(_gift.DOPunchScale(Vector3.one * 0.3f, 0.5f, 1))
            .Join(_gift.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.75f));
        _ui.coins += _reward;
    }

    private void Active()
    {
        GetComponent<Button>().interactable = true;
    }
}
