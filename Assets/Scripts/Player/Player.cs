using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    [Header("Player Statistic")]
    [SerializeField] private float _speed;
    [SerializeField] private int _currentScore = 0;
    [SerializeField] private int _highScore = 0;

    [SerializeField] private int _coinAmount = 1;

    [Header("Bullet Settings")]
    [SerializeField] GameObject _bulletPrefab;
    private float nextFireTime = 0f;

    [Header("Scriptable Object")]
    [SerializeField] private PlayerData _playerData;

    [Header("UI Menu")]
    public GameObject GameOverUI;
    public GameObject PauseButtonUI;
    public GameObject ScoreUI;
    [SerializeField] private TextMeshProUGUI _scoreTextUI;
    [SerializeField] private TextMeshProUGUI _highScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;

    [SerializeField] private TextMeshProUGUI _walletAmountTextUI;
    [SerializeField] private TextMeshProUGUI _currentCoinsEarnedUI;

    private PlayerController _controller;
    private BulletSpawn _bulletSpawner;
    private PlayerEarnings _earnings;
    private void Start()
    {
        UpdateScoreUI();

        ScoreUI.SetActive(true);

        _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        _walletAmountTextUI.text = $"WALLET: {_playerData.WalletAmount}";
        _currentCoinsEarnedUI.text = $"CURRENT COINS EARNED: 0";

        _controller = GetComponentInChildren<PlayerController>();

        _bulletSpawner = GetComponentInChildren<BulletSpawn>();

        _earnings = GetComponent<PlayerEarnings>();
    }
    private void Update()
    {
        _controller.Movement(_speed, gameObject);

        if (Time.time >= nextFireTime)
        {
            _bulletSpawner.Spawn(_bulletPrefab);
            nextFireTime = Time.time + _playerData.FireRate;
        }
    }

    #region SavePlayerPrefsSystem
    public void UpdateScoreUI()
    {
        _scoreTextUI.text = $"SCORE: {_currentScore}";
        _currentScoreTextUI.text = $"CURRENT SCORE: {_currentScore}";
    }

    public void UpdateCoinUI(int numberCoins)
    {
        _currentCoinsEarnedUI.text = $"CURRENT COINS EARNED: {numberCoins}";
    }

    public void AddScore(int value)
    {
        _currentScore += value;

        UpdateScoreUI();
    }
    public void AddWallet()
    {
        int numberCoins = _currentScore * _coinAmount / 2;
        _playerData.WalletAmount += numberCoins;

        UpdateCoinUI(numberCoins);
    }

    public void CheckAndSaveHighScore()
    {
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            PlayerPrefs.SetInt(HighScoreKey, _highScore);
            PlayerPrefs.Save();
            _highScoreTextUI.text = $"High Score: {_highScore}";
        }
        else
        {
            _highScoreTextUI.text = $"High Score: {_highScore}";
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, 0);
        PlayerPrefs.Save();
    }
    #endregion
}
