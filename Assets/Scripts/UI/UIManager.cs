using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameData _gameData;
    [SerializeField] private PlayerData _playerData;

    [Header("GameObjects UI")]
    public GameObject VictoryUI;
    public GameObject GameOverUI;
    public GameObject PauseButton;
    public GameObject HUD;

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
        HUD.SetActive(true);

        _LevelPanel.color = _gameData.LevelsColor[_gameData.CurrentLevel];
        _levelName.text = $"«ŒÕ¿";
        _stageCount.text = $"›“¿œ - {_gameData.CurrentStage}/{_gameData.TotalStage}";
    }

    public void UpdateVictoryGameUI()
    {
        VictoryUI.SetActive(true);
        HUD.SetActive(false);
        PauseButton.SetActive(false);

        _currentStageUI.text = $"œ–Œ…ƒ≈ÕŒ ›“¿œŒ¬: {_gameData.CurrentStage} ËÁ {_gameData.TotalStage}";
        _recordStageAfterVictoryUI.text = $"¬—≈√Œ œ–Œ…ƒ≈ÕÕ€’ ›“¿œŒ¬: {_gameData.RecordStage}";
        _walletAmountAfterVictoryUI.text = $"√–”«Œ¬Œ… Œ“—≈ : {_playerData.WalletAmount}";
        _coinsEarnedAfterVictoryUI.text = $"—Œ¡–¿ÕŒ: {_playerData.Collected—oinsAmount}";
    }
    public void UpdateGameOverUI()
    {
        GameOverUI.SetActive(true);
        HUD.SetActive(false);
        PauseButton.SetActive(false);

        _recordStageAfterDeathUI.text = $"¬—≈√Œ œ–Œ…ƒ≈ÕÕ€’ ›“¿œŒ¬: {_gameData.RecordStage}";
        _walletAmountAfterDeathUI.text = $"√–”«Œ¬Œ… Œ“—≈ : {_playerData.WalletAmount}";
        _coinsEarnedAfterDeathUI.text = $"—Œ¡–¿ÕŒ: {_playerData.Collected—oinsAmount}";
    }

    public void SetTimerText(string text)
    {
        _timer.text = text;
    }
}
