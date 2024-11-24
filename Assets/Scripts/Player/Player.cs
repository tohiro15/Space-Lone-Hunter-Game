using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    [Header("Player Statistic")]
    [SerializeField] private float _speed;
    [SerializeField] private int _currentScore = 0;
    public int HighScore { get; private set; } = 0;

    [Header("Bullet Settings")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime = 0f;

    [Header("UI Menu")]
    public GameObject GameOverUI;
    public GameObject ScoreUI;
    [SerializeField] private TextMeshProUGUI _scoreTextUI;
    [SerializeField] private TextMeshProUGUI _highScoreTextUI;

    private PlayerController _controller;
    private BulletSpawn _bulletSpawner;
    private void Start()
    {
        UpdateScoreUI();

        ScoreUI.SetActive(true);

        HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        _controller = GetComponentInChildren<PlayerController>();

        _bulletSpawner = GetComponentInChildren<BulletSpawn>();
    }
    private void Update()
    {
        _controller.Movement(_speed, gameObject);

        if (Time.time >= nextFireTime)
        {
            _bulletSpawner.Spawn(_bulletPrefab);
            nextFireTime = Time.time + fireRate;
        }
    }

    #region SavePlayerPrefsSystem
    public void UpdateScoreUI()
    {
        _scoreTextUI.text = $"SCORE: {_currentScore}";
    }

    public void AddScore(int value)
    {
        _currentScore += value;
        UpdateScoreUI();
    }

    public void CheckAndSaveHighScore()
    {
        if (_currentScore > HighScore)
        {
            HighScore = _currentScore;
            PlayerPrefs.SetInt(HighScoreKey, HighScore);
            PlayerPrefs.Save();
            _highScoreTextUI.text = $"High Score: {HighScore}";
        }
        else
        {
            _highScoreTextUI.text = $"High Score: {HighScore}";
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, 0);
        PlayerPrefs.Save();
    }
    #endregion
}
