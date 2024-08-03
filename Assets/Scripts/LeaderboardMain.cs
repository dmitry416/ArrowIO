using UnityEngine;
using YG;
using YG.Utils.LB;

public class LeaderboardMain : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private LeaderboardItem _first;
    [SerializeField] private LeaderboardItem _second;
    [SerializeField] private LeaderboardItem _third;
    [SerializeField] private LeaderboardItem _other;
    private bool isUpdated = false;

    private void OnEnable()
    {
        YandexGame.onGetLeaderboard += OnUpdateLB;
        YandexGame.GetLeaderboard("rating", 25, 15, 10, "");
    }

    private void OnDisable() => YandexGame.onGetLeaderboard -= OnUpdateLB;

    private void OnUpdateLB(LBData data)
    {
        if (isUpdated)
            return;
        isUpdated = true;
        LeaderboardItem place;
        print(data.players.Length);
        for (int i = 0; i < data.players.Length; ++i)
        {
            switch (i)
            {
                case 0:
                    place = Instantiate(_first, _content);
                    place.SetPlace(data.players[i].rank, data.players[i].name, data.players[i].score, true);
                    break;
                case 1:
                    place = Instantiate(_second, _content);
                    place.SetPlace(data.players[i].rank, data.players[i].name, data.players[i].score, true);
                    break;
                case 2:
                    place = Instantiate(_third, _content);
                    place.SetPlace(data.players[i].rank, data.players[i].name, data.players[i].score, true);
                    break;
                default:
                    place = Instantiate(_other, _content);
                    place.SetPlace(data.players[i].rank, data.players[i].name, data.players[i].score, false);
                    break;
            }
        }
    }
}
