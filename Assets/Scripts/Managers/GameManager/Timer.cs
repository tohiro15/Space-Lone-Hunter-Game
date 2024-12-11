using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _stageDuration = 60f;
    [SerializeField] private PlayerVictory _playerVictoryScript;
    [SerializeField] private UIManager _uiManager;

    void Start()
    {
        StartCoroutine(TimerToVictory());
    }
    IEnumerator TimerToVictory()
    {
        float currentTime = _stageDuration;
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
