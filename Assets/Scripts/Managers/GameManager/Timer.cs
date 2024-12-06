using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerVictory _playerVictoryScript;
    [SerializeField] private UIManager _uiManager;

    void Start()
    {
        StartCoroutine(TimerToVictory());
    }
    IEnumerator TimerToVictory()
    {
        float currentTime = _playerData.StageDuration;
        while (currentTime > 0)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            _uiManager.SetTimerText($"{minutes}:{seconds:00}");

            currentTime -= Time.deltaTime;
            yield return null;
        }
        _playerVictoryScript.Victory();
    }
}
