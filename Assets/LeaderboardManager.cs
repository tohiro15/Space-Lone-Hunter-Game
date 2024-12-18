using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _playerNicknames;
    [SerializeField] private List<TextMeshProUGUI> _scores;

    private string _leaderboardID = "Space_Lone_Hunter";

}
