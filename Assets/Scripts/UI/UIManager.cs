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
    [SerializeField] private TextMeshProUGUI _walletAmountTextUI;
    [Header("GameOver UI Elements")]
    [SerializeField] private TextMeshProUGUI _walletAmountAfterDeathTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;

    private void Start()
    {
        _walletAmountTextUI.text = $"WALLET: {_playerData.WalletAmount}";
        _currentCoinsEarnedUI.text = $"CURRENT COINS EARNED: 0";

        HUD.SetActive(true);
    }

    public void UpdateCoinUI(int amount)
    {
        _currentCoinsEarnedUI.text = $"COLLECT COINS AMOUNT: {amount}";
        _walletAmountTextUI.text = $"WALLET: {_playerData.WalletAmount}";
        _walletAmountAfterDeathTextUI.text = _walletAmountTextUI.text;
    }

    public void SetCurrentCoinsEarnedUI(string text)
    {
        _currentCoinsEarnedUI.text = text;
    }
    public void SetWalletAmountTextUI(string text)
    {
        _walletAmountTextUI.text = text;
    }
    public void SetWalletAmountAfterDeathTextUI(string text)
    {
        _walletAmountAfterDeathTextUI.text = text;
    }
}
