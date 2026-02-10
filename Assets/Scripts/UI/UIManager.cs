using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private PlayerData _playerData;

    [Header("GameObjects UI")]
    [SerializeField] private GameObject _victoryUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _hud;

    [Header("HUD UI Elements")]
    [SerializeField] private Image _LevelPanel;
    [SerializeField] private TextMeshProUGUI _stageCount;
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private TextMeshProUGUI _timer;

    [Header("Victory Game UI Elements")]
    [SerializeField] private TextMeshProUGUI _currentStageUI;
    [SerializeField] private TextMeshProUGUI _recordStageAfterVictoryUI;
    [SerializeField] private TextMeshProUGUI _walletAmountAfterVictoryUI;
    [SerializeField] private TextMeshProUGUI _coinsEarnedAfterVictoryUI;

    [Header("GameOver UI Elements")]
    [SerializeField] private TextMeshProUGUI _recordStageAfterDeathUI;
    [SerializeField] private TextMeshProUGUI _walletAmountAfterDeathUI;
    [SerializeField] private TextMeshProUGUI _coinsEarnedAfterDeathUI;
    private void Start()
    {
        _hud?.gameObject.SetActive(true);

        _LevelPanel.color = _gameData.LevelsColor[_gameData.CurrentLevel];
        _levelName.text = $"«ŒÕ¿";
        _stageCount.text = $"›“¿œ - {_gameData.CurrentStage}/{_gameData.TotalStage}";
    }

    public void UpdateVictoryGameUI()
    {
        _victoryUI?.gameObject.SetActive(true);
        _hud?.gameObject.SetActive(false);
        _pauseButton?.gameObject.SetActive(false);

        _currentStageUI.text = $"{_gameData.CurrentStage} ËÁ {_gameData.TotalStage}";
        _recordStageAfterVictoryUI.text = _gameData.RecordStage.ToString();
        _walletAmountAfterVictoryUI.text = _playerData.WalletAmount.ToString();
        _coinsEarnedAfterVictoryUI.text = _playerData.CollectedCoinsAmount.ToString();
    }
    public void UpdateGameOverUI()
    {
        _gameOverUI?.gameObject.SetActive(true);
        _hud?.gameObject.SetActive(false);
        _pauseButton?.gameObject.SetActive(false);

        _recordStageAfterDeathUI.text = _gameData.RecordStage.ToString();
        _walletAmountAfterDeathUI.text = _playerData.WalletAmount.ToString();
        _coinsEarnedAfterDeathUI.text = _playerData.CollectedCoinsAmount.ToString();
    }

    public void SetTimerText(string text)
    {
        _timer.text = text;
    }
}
