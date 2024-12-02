using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    [Header("GameObjects UI")]
    public GameObject GameOverUI;
    public GameObject PauseButton;
    public GameObject HUD;

    [Header("Game UI Elements")]
    [SerializeField] private TextMeshProUGUI _currentCoinsEarnedUI;

    [Header("GameOver UI Elements")]
    [SerializeField] private TextMeshProUGUI _walletAmountAfterDeathTextUI;
    [SerializeField] private TextMeshProUGUI _coinsEarnedUI;

    private void Start()
    {
        _currentCoinsEarnedUI.text = $"COLLECTED COINS: 0";

        HUD.SetActive(true);
    }

    public void UpdateCoinUI(int amount)
    {
        _currentCoinsEarnedUI.text = $"COLLECTED COINS: {amount}";
    }

    public void UpdateGameOverUI()
    {
        _coinsEarnedUI.text = _currentCoinsEarnedUI.text;
        _walletAmountAfterDeathTextUI.text = $"WALLET: {_playerData.WalletAmount}";
    }
    public void SetCurrentCoinsEarnedUI(string text)
    {
        _currentCoinsEarnedUI.text = text;
    }
    public void SetWalletAmountAfterDeathTextUI(string text)
    {
        _walletAmountAfterDeathTextUI.text = text;
    }
}
