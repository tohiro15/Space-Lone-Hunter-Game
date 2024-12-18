using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private GameData _gameData;

    private void Awake()
    {
        _gameData.CurrentLevel = PlayerPrefs.GetInt(GameData.LEVEL_CURRENT_KEY, 0);

        _gameData.CurrentStage = PlayerPrefs.GetInt(GameData.STAGE_CURRENT_KEY, 1);
        _gameData.TotalStage = PlayerPrefs.GetInt(GameData.STAGE_TOTAL_KEY, 5);
        _gameData.RecordStage = PlayerPrefs.GetInt(GameData.RECORD_STAGE_KEY, 0);

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

        if (_playerData != null && _gameData != null)
        {
            _gameData.CurrentLevel = 0;
            _gameData.CurrentStage = 1;
            _gameData.TotalStage = 5;

            _playerData.CollectedСoinsAmount = 0;

            PlayerPrefs.SetInt(GameData.LEVEL_CURRENT_KEY, _gameData.CurrentLevel);
            PlayerPrefs.SetInt(GameData.STAGE_CURRENT_KEY, _gameData.CurrentStage);
            PlayerPrefs.SetInt(GameData.STAGE_TOTAL_KEY, _gameData.TotalStage);
            PlayerPrefs.SetInt(GameData.RECORD_STAGE_KEY, _gameData.RecordStage);
            PlayerPrefs.SetInt(PlayerData.WALLET_AMOUNT_KEY, _playerData.WalletAmount);
            PlayerPrefs.SetInt(PlayerData.COLLECTED_COINS_AMOUNT_KEY, _playerData.CollectedСoinsAmount);

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

        if (_playerData != null && _gameData != null)
        {
            _gameData.CurrentStage += 1;
            _gameData.RecordStage++;
            _playerData.CollectedСoinsAmount = 0;

            if(_gameData.CurrentStage > _gameData.TotalStage)
            {
                _gameData.CurrentLevel++;
                _gameData.CurrentLevel = Mathf.Clamp(_gameData.CurrentLevel, 0, _gameData.LevelsName.Length - 1);

                PlayerPrefs.SetInt(GameData.LEVEL_CURRENT_KEY, _gameData.CurrentLevel);

                _gameData.CurrentStage = 1;
                _gameData.TotalStage += 5;

                PlayerPrefs.SetInt(GameData.STAGE_TOTAL_KEY, _gameData.TotalStage);
            }

            PlayerPrefs.SetInt(GameData.STAGE_CURRENT_KEY, _gameData.CurrentStage);
            PlayerPrefs.SetInt(GameData.RECORD_STAGE_KEY, _gameData.RecordStage);
            PlayerPrefs.SetInt(PlayerData.WALLET_AMOUNT_KEY, _playerData.WalletAmount);
            PlayerPrefs.SetInt(PlayerData.COLLECTED_COINS_AMOUNT_KEY, _playerData.CollectedСoinsAmount);

            PlayerPrefs.Save();
            Debug.Log("Данные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData и GameData не инициализированы.");
        }
    }

    public void SaveDataAfterClosedGame()
    {
        if (_playerData != null && _gameData != null)
        {
            _playerData.CollectedСoinsAmount = 0;

            PlayerPrefs.SetInt(GameData.LEVEL_CURRENT_KEY, _gameData.CurrentLevel);
            PlayerPrefs.SetInt(GameData.STAGE_CURRENT_KEY, _gameData.CurrentStage);
            PlayerPrefs.SetInt(GameData.STAGE_TOTAL_KEY, _gameData.TotalStage);
            PlayerPrefs.SetInt(GameData.RECORD_STAGE_KEY, _gameData.RecordStage);
            PlayerPrefs.SetInt(PlayerData.WALLET_AMOUNT_KEY, _playerData.WalletAmount);
            PlayerPrefs.SetInt(PlayerData.COLLECTED_COINS_AMOUNT_KEY, _playerData.CollectedСoinsAmount);

            PlayerPrefs.Save();
            Debug.Log("Данные игрока сохранены в PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData и GameData не инициализированы.");
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
