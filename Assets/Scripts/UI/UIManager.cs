using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    [Header("GameObjects UI")]
    public GameObject VictoryUI;
    public GameObject GameOverUI;
    public GameObject PauseButton;
    public GameObject HUD;

    [Header("Game UI Elements")]
    [SerializeField] private TextMeshProUGUI _stageCount;
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

        _stageCount.text = $"STAGE {_playerData.StagePassed.ToString()}";
    }

    public void UpdateVictoryGameUI()
    {
        VictoryUI.SetActive(true);
        HUD.SetActive(false);
        PauseButton.SetActive(false);

        _currentStageUI.text = $"CURRENT STAGE:{_playerData.StagePassed}";
        _recordStageAfterVictoryUI.text = $"RECORD STAGE:{_playerData.RecordStage}";
        _walletAmountAfterVictoryUI.text = $"WALLET:{_playerData.WalletAmount}";
        _coinsEarnedAfterVictoryUI.text = $"EARNED:{_playerData.Collected—oinsAmount}";
    }
    public void UpdateGameOverUI()
    {
        GameOverUI.SetActive(true);
        HUD.SetActive(false);
        PauseButton.SetActive(false);

        _recordStageAfterDeathUI.text = $"RECORD STAGE : {_playerData.RecordStage}";
        _walletAmountAfterDeathUI.text = $"WALLET: {_playerData.WalletAmount}";
        _coinsEarnedAfterDeathUI.text = $"EARNED: {_playerData.Collected—oinsAmount}";
    }

    public void SetTimerText(string text)
    {
        _timer.text = text;
    }
}
