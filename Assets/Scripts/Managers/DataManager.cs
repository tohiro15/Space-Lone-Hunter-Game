using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnemyData _enemyData;

    private void Start()
    {
        if(_playerData.StagePassed <= 1) _enemyData.Health = 1;
    }
    public void AddWallet(int value)
    {
        _playerData.CollectedСoinsAmount++;

        PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount += value);
    }

    public void SaveDataAfterDeath()
    {
        _playerData.CollectedСoinsAmount = 0;
        _enemyData.Health = 0;

        if (_playerData != null)
        {
            if (_playerData.StagePassed > _playerData.RecordStage)
            {
                _playerData.RecordStage = _playerData.StagePassed - 1;
                PlayerPrefs.SetInt(PlayerData.RecordStageKey, _playerData.RecordStage);
            }

            _playerData.StagePassed = 1;

            PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount);
            PlayerPrefs.SetInt(PlayerData.StagePassedKey, 1);

            PlayerPrefs.Save();
            Debug.Log("Данные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData не инициализирован.");
        }
    }
    public void SaveDataAfterVictory()
    {
        _playerData.CollectedСoinsAmount = 0;

        if (_playerData != null)
        {
            PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount);

            PlayerPrefs.Save();
            Debug.Log("Данные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData не инициализирован.");
        }
    }

    public void SaveDataAfterShop()
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
