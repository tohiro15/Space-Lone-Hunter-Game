using UnityEngine;
using System.Collections;
using TMPro;

public class PauseGame : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _secondsToReturn = 5;

    [Header("Timer UI")]
    private TextMeshProUGUI _timer;

    [Header ("UI objects")]
    [SerializeField] private GameObject _timerUI;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _HUD;

    private void Start()
    {
        _pauseMenuUI.SetActive(false);
        _HUD.SetActive(true);
        _timer = _timerUI.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        _HUD.SetActive(false);
        _pauseMenuUI.SetActive(true);
    }

    public void ContinuePlaying()
    {
        _pauseMenuUI.SetActive(false);
        _timerUI.SetActive(true);
        StartCoroutine(TimerToReturn());
    }

    IEnumerator TimerToReturn()
    {
        int seconds = _secondsToReturn;
        while (seconds > 0)
        {
            _timer.text = $"{seconds}";
            yield return new WaitForSecondsRealtime(1);
            seconds--;
        }

        Time.timeScale = 1;

        _timerUI.SetActive(false);
        _HUD.SetActive(true);
    }
}
