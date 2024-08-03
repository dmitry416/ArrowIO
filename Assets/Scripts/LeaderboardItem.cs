using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nick;
    [SerializeField] private TextMeshProUGUI _score;

    public void SetPlace(int place, string nick, int score, bool isMedal)
    {
        if (isMedal)
            _nick.text = nick;
        else
            _nick.text = $"{place}. {nick}";
        _score.text = score.ToString();
    }
}
