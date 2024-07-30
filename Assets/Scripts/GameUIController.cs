using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _coinsEarned;
    [SerializeField] private RectTransform _bg;
    [SerializeField] private RectTransform _chest;
    [SerializeField] private RectTransform _button;
    

    public void EndPanel(string title)
    {
        PlusToSkin();
        _title.text = title;
        int lvl = FindObjectOfType<PlayerController>().GetComponent<CharacterController>()._lvl;
        int collectedCoins = lvl > 2 ? lvl * 50 : 0;
        int rating = lvl > 2 ? (int)Mathf.Pow(lvl, 2) : 0;
        YandexGame.savesData.coins += collectedCoins;
        YandexGame.savesData.rating += rating;
        YandexGame.SaveProgress();
        YandexGame.NewLeaderboardScores("rating", YandexGame.savesData.rating);
        _coinsEarned.text = $"+{collectedCoins} монет\n+{rating} рейтинг";
        _bg.localScale = _bg.localScale - Vector3.right;
        _title.transform.localScale = Vector3.zero;
        _coinsEarned.transform.localScale = Vector3.zero;
        _chest.transform.localScale = Vector3.zero;
        _button.transform.localScale = Vector3.zero;
        _endPanel.SetActive(true);
        DOTween.Sequence()
            .AppendInterval(2)
            .Append(_endPanel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0.5f), 0.5f))
            .Append(_bg.DOScaleX(1, 0.5f))
            .Append(_title.transform.DOScale(Vector3.one, 0.75f))
            .Append(_coinsEarned.transform.DOScale(Vector3.one, 0.75f))
            .Join(_chest.DOScale(Vector3.one, 0.75f))
            .Append(_button.DOScale(Vector3.one, 0.75f));
    }

    public void PlusToSkin()
    {
        YandexGame.savesData.openSkins[YandexGame.savesData.selectedSkin]++;
        YandexGame.SaveProgress();
    }
}
