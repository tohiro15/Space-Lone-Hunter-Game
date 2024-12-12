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
        _playerData.StagePassed = PlayerPrefs.GetInt(PlayerData.StagePassedKey, 1);
        _playerData.RecordStage = PlayerPrefs.GetInt(PlayerData.RecordStageKey, 0);

        _playerData.WalletAmount = PlayerPrefs.GetInt(PlayerData.WalletAmountKey, 0);
        _playerData.CollectedÑoinsAmount = PlayerPrefs.GetInt(PlayerData.CollectedÑoinsAmountKey, 0);

        _playerData.FireRate = PlayerPrefs.GetFloat(PlayerData.FireRateKey, 1);

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
        _playerData.CollectedÑoinsAmount++;

        PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount += value);
    }

    public void SaveDataAfterDeath()
    {

        if (_playerData != null)
        {
            if (_playerData.StagePassed > _playerData.RecordStage)
            {
                PlayerPrefs.SetInt(PlayerData.RecordStageKey, _playerData.RecordStage = _playerData.StagePassed - 1);
            }

            PlayerPrefs.SetInt(PlayerData.StagePassedKey, _playerData.StagePassed = 1);
            PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount);
            PlayerPrefs.SetInt(PlayerData.CollectedÑoinsAmountKey, _playerData.CollectedÑoinsAmount = 0);

            PlayerPrefs.Save();
            Debug.Log("Äàííûå èãğîêà ñîõğàíåíû â PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData íå èíèöèàëèçèğîâàí.");
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

            PlayerPrefs.SetInt(PlayerData.StagePassedKey, _playerData.StagePassed++);
            PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount);
            PlayerPrefs.SetInt(PlayerData.CollectedÑoinsAmountKey, _playerData.CollectedÑoinsAmount = 0);

            PlayerPrefs.Save();
            Debug.Log("Äàííûå èãğîêà ñîõğàíåíû â PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData íå èíèöèàëèçèğîâàí.");
        }
    }

    public void SaveDataAfterShop()
    {
        if (_playerData != null)
        {
            PlayerPrefs.SetInt(PlayerData.WalletAmountKey, _playerData.WalletAmount);
            PlayerPrefs.SetFloat(PlayerData.FireRateKey, _playerData.FireRate);

            PlayerPrefs.Save();
            Debug.Log("Äàííûå èãğîêà ñîõğàíåíû â PlayerPrefs.");
        }
        else
        {
            Debug.LogError("PlayerData íå èíèöèàëèçèğîâàí.");
        }
    }
}
