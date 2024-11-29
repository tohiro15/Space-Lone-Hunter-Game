using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    [Header("Player Statistic")]
    [SerializeField] private float _speed;
    [SerializeField] private int _currentScore = 0;
    [SerializeField] private int _highScore = 0;

    [Header("Bullet Settings")]
    [SerializeField] GameObject _bulletPrefab;
    private float nextFireTime = 0f;

    [Header("Scriptable Object")]
    [SerializeField] private PlayerData _playerData;

    [Header("GameObjects UI")]
    public GameObject GameOverUI;
    public GameObject PauseButtonUI;
    public GameObject ScoreUI;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _scoreTextUI;
    [SerializeField] private TextMeshProUGUI _highScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;
    [SerializeField] private TextMeshProUGUI _walletAmountAfterDeathTextUI;

    [SerializeField] private TextMeshProUGUI _walletAmountTextUI;
    [SerializeField] private TextMeshProUGUI _currentCoinsEarnedUI;

    private PlayerController _controller;
    private BulletSpawn _bulletSpawner;
    private void Start()
    {
        UpdateScoreUI();

        ScoreUI.SetActive(true);

        _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        _walletAmountTextUI.text = $"WALLET: {_playerData.WalletAmount}";
        _currentCoinsEarnedUI.text = $"CURRENT COINS EARNED: 0";

        _controller = GetComponentInChildren<PlayerController>();

        _bulletSpawner = GetComponentInChildren<BulletSpawn>();
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

    public void UpdateCoinUI(int newAmount)
    {
        _currentCoinsEarnedUI.text = $"CURRENT COINS EARNED: {newAmount}";
        _walletAmountAfterDeathTextUI.text = $"Wallet: {_playerData.WalletAmount}";
    }

    public void AddScore(int value)
    {
        _currentScore += value;

        UpdateScoreUI();
    }
    public void AddWallet()
    {
        int newAmount = _currentScore / 2;
        _playerData.WalletAmount += newAmount;
        UpdateCoinUI(newAmount);
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
