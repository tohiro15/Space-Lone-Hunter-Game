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
            PlayerPrefs.SetInt(PlayerData.RecordStageKey, _playerData.RecordStage);
            PlayerPrefs.SetInt(PlayerData.StagePassedKey, _playerData.StagePassed);
            PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount);
            PlayerPrefs.SetFloat(PlayerData.StageDurationKey, _playerData.StageDuration);
            PlayerPrefs.SetFloat(PlayerData.FireRateKey, _playerData.FireRate);

            PlayerPrefs.Save();
            Debug.Log("Данные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData не инициализирован.");
        }
    }
    public void SavePlayerDataAfterDeath()
    {
        if (_playerData != null)
        {
            if (_playerData.StagePassed > _playerData.RecordStage)
            {
                _playerData.RecordStage = _playerData.StagePassed;
                PlayerPrefs.SetInt(PlayerData.RecordStageKey, _playerData.RecordStage);
            }

            _playerData.StagePassed = 1;

            PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount);
            PlayerPrefs.SetInt(PlayerData.StagePassedKey, _playerData.StagePassed);

            PlayerPrefs.SetFloat(PlayerData.FireRateKey, _playerData.FireRate);

            PlayerPrefs.DeleteKey(PlayerData.StageDurationKey);
            PlayerPrefs.DeleteKey(PlayerData.CollectedСoinsAmountKey);

            PlayerPrefs.Save();
            Debug.Log("Данные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData не инициализирован.");
        }
    }

    public void SavePlayerDataAfterShop()
    {
        if (_playerData != null)
        {
            PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount);
            PlayerPrefs.SetFloat(PlayerData.FireRateKey, _playerData.FireRate);

            PlayerPrefs.Save();
            Debug.Log("Данные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData не инициализирован.");
        }
    }
}
