using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnemyData _enemyData;

    private void Awake()
    {
        _playerData.StagePassed = PlayerPrefs.GetInt(PlayerData.STAGE_PASSED_KEY, 1);
        _playerData.RecordStage = PlayerPrefs.GetInt(PlayerData.RECORD_STAGE_KEY, 0);

        _playerData.WalletAmount = PlayerPrefs.GetInt(PlayerData.WALLET_AMOUNT_KEY, 0);
        _playerData.CollectedСoinsAmount = PlayerPrefs.GetInt(PlayerData.COLLECTED_COINS_AMOUNT_KEY, 0);

        _playerData.BulletCount = PlayerPrefs.GetInt(PlayerData.BULLET_COUNT_KEY, 1);

        _playerData.FireRate = PlayerPrefs.GetFloat(PlayerData.FIRE_RATE_KEY, 1);
        _playerData.FireDamage = PlayerPrefs.GetInt(PlayerData.FIRE_DAMAGE_KEY, 1);

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddWallet(int value)
    {
        _playerData.CollectedСoinsAmount++;

        PlayerPrefs.SetInt(PlayerData.WALLET_AMOUNT_KEY, _playerData.WalletAmount += value);
    }

    public void SaveDataAfterDeath()
    {

        if (_playerData != null)
        {
            if (_playerData.StagePassed > _playerData.RecordStage)
            {
                PlayerPrefs.SetInt(PlayerData.RECORD_STAGE_KEY, _playerData.RecordStage = _playerData.StagePassed - 1);
            }

            PlayerPrefs.SetInt(PlayerData.STAGE_PASSED_KEY, _playerData.StagePassed = 1);
            PlayerPrefs.SetInt(PlayerData.WALLET_AMOUNT_KEY, _playerData.WalletAmount);
            PlayerPrefs.SetInt(PlayerData.COLLECTED_COINS_AMOUNT_KEY, _playerData.CollectedСoinsAmount = 0);

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

        if (_playerData != null)
        {
            if (_playerData.StagePassed > _playerData.RecordStage)
            {
                _playerData.RecordStage = _playerData.StagePassed;
            }

            PlayerPrefs.SetInt(PlayerData.STAGE_PASSED_KEY, _playerData.StagePassed += 1);
            PlayerPrefs.SetInt(PlayerData.WALLET_AMOUNT_KEY, _playerData.WalletAmount);
            PlayerPrefs.SetInt(PlayerData.COLLECTED_COINS_AMOUNT_KEY, _playerData.CollectedСoinsAmount = 0);

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
            PlayerPrefs.SetInt(PlayerData.WALLET_AMOUNT_KEY, _playerData.WalletAmount);

            PlayerPrefs.SetInt(PlayerData.BULLET_COUNT_KEY, _playerData.BulletCount);

            PlayerPrefs.SetInt(PlayerData.FIRE_DAMAGE_KEY, _playerData.FireDamage);
            PlayerPrefs.SetFloat(PlayerData.FIRE_RATE_KEY, _playerData.FireRate);


            PlayerPrefs.Save();
            Debug.Log("Данные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData не инициализирован.");
        }
    }
}
