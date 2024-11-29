using TMPro;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private TextMeshProUGUI _walletAmountTextUI;

    private const string WalletAmountKey = "WalletAmount";
    private const string FireRateKey = "FireRate";

    private void Start()
    { 
        _playerData.WalletAmount = PlayerPrefs.GetInt(WalletAmountKey, 0);
        _playerData.FireRate = PlayerPrefs.GetFloat(FireRateKey, 1);

        _walletAmountTextUI.text = $"WALLET: {_playerData.WalletAmount}";
    }
    public void SavePlayerData()
    {
        if (_playerData != null)
        {
            PlayerPrefs.SetInt(WalletAmountKey, _playerData.WalletAmount);
            PlayerPrefs.SetFloat(FireRateKey, _playerData.FireRate);

            PlayerPrefs.Save();
            Debug.Log("ƒанные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData не инициализирован.");
        }
    }
}

