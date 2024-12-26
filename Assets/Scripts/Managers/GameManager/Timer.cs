using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _stageDuration = 60f;
    [SerializeField] private PlayResult _victoryScript;
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
        _victoryScript.Victory();
    }
}
