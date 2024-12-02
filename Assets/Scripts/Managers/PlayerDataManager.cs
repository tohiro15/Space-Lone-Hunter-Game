using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    public void SavePlayerData()
    {
        if (_playerData != null)
        {
            PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount);
            PlayerPrefs.SetFloat(PlayerData.FireRateKey, _playerData.FireRate);

            PlayerPrefs.Save();
            Debug.Log("ƒанные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData не инициализирован.");
        }
    }
}
