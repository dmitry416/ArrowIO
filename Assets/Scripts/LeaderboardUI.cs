using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name1;
    [SerializeField] private TextMeshProUGUI _name2;
    [SerializeField] private TextMeshProUGUI _name3;
    [SerializeField] private TextMeshProUGUI _lvl1;
    [SerializeField] private TextMeshProUGUI _lvl2;
    [SerializeField] private TextMeshProUGUI _lvl3;

    public int first = 0, second = 0, third = 0;

    public void UpdateLeaderboard()
    {
        List<CharacterController> chars = FindObjectsOfType<CharacterController>().OrderBy(o => -o._lvl).ToList();
        switch (chars.Count)
        {
            case 0:
                break;
            case 1:
                SetFirst(chars[0]._nick, chars[0]._lvl);
                break;
            case 2:
                SetFirst(chars[0]._nick, chars[0]._lvl);
                SetSecond(chars[1]._nick, chars[1]._lvl);
                break;
            default:
                SetFirst(chars[0]._nick, chars[0]._lvl);
                SetSecond(chars[1]._nick, chars[1]._lvl);
                SetThird(chars[2]._nick, chars[2]._lvl);
                break;
        }
    }

    public void SetFirst(string name, int lvl)
    {
        _name1.text = name;
        _lvl1.text = lvl.ToString();
        first = lvl;
    }

    public void SetSecond(string name, int lvl)
    {
        _name2.text = name;
        _lvl2.text = lvl.ToString();
        second = lvl;
    }

    public void SetThird(string name, int lvl)
    {
        _name3.text = name;
        _lvl3.text = lvl.ToString();
        third = lvl;
    }
}
