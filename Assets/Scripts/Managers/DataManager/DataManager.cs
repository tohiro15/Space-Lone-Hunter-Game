using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameData _gameData;

    private const int DefaultLevel = 0;
    private const int DefaultStage = 1;
    private const int DefaultTotalStage = 5;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadGameData();
    }

    private void LoadGameData()
    {
        _gameData.CurrentLevel = PlayerPrefs.GetInt(GameData.LEVEL_CURRENT_KEY, DefaultLevel);
        _gameData.CurrentStage = PlayerPrefs.GetInt(GameData.STAGE_CURRENT_KEY, DefaultStage);
        _gameData.TotalStage = PlayerPrefs.GetInt(GameData.STAGE_TOTAL_KEY, DefaultTotalStage);
        _gameData.RecordStage = PlayerPrefs.GetInt(GameData.RECORD_STAGE_KEY, 0);
        _gameData.ImprovementsUnlocked = PlayerPrefs.GetInt(GameData.IMPROVEMENT_KEY, 1);

        _playerData.WalletAmount = PlayerPrefs.GetInt(PlayerData.WALLET_AMOUNT_KEY, 0);
        _playerData.CollectedCoinsAmount = PlayerPrefs.GetInt(PlayerData.COLLECTED_COINS_AMOUNT_KEY, 0);
        _playerData.BulletCount = PlayerPrefs.GetInt(PlayerData.BULLET_COUNT_KEY, 1);
        _playerData.FireRate = PlayerPrefs.GetFloat(PlayerData.FIRE_RATE_KEY, 1f);
        _playerData.FireDamage = PlayerPrefs.GetFloat(PlayerData.FIRE_DAMAGE_KEY, 1f);
    }

    public void AddWallet(int value)
    {
        _playerData.CollectedCoinsAmount++;
        _playerData.WalletAmount += value;

        SaveData(PlayerData.WALLET_AMOUNT_KEY, _playerData.WalletAmount);
    }

    private void SaveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    private void SaveData(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    private void SaveGameData()
    {
        PlayerPrefs.SetInt(GameData.LEVEL_CURRENT_KEY, _gameData.CurrentLevel);
        PlayerPrefs.SetInt(GameData.STAGE_CURRENT_KEY, _gameData.CurrentStage);
        PlayerPrefs.SetInt(GameData.STAGE_TOTAL_KEY, _gameData.TotalStage);
        PlayerPrefs.SetInt(GameData.RECORD_STAGE_KEY, _gameData.RecordStage);
        PlayerPrefs.SetInt(GameData.IMPROVEMENT_KEY, _gameData.ImprovementsUnlocked);
    }

    public void SaveDataAfterDeath()
    {
        ResetGameData();
        SaveAllPlayerData();
    }

    public void SaveDataAfterVictory()
    {
        _gameData.CurrentStage++;
        _gameData.RecordStage++;

        if (_gameData.CurrentStage > _gameData.TotalStage)
        {
            _gameData.CurrentLevel++;
            _gameData.CurrentLevel = Mathf.Clamp(_gameData.CurrentLevel, 0, _gameData.LevelsColor.Length - 1);
            _gameData.ImprovementsUnlocked = Mathf.Max(_gameData.ImprovementsUnlocked, _gameData.CurrentLevel + 1);
            PlayerPrefs.SetInt(GameData.IMPROVEMENT_KEY, _gameData.ImprovementsUnlocked);
            PlayerPrefs.SetInt(GameData.LEVEL_CURRENT_KEY, _gameData.CurrentLevel);

            _gameData.CurrentStage = 1;
            _gameData.TotalStage += 5;
            _playerData.WalletAmount += 100;
        }

        _playerData.CollectedCoinsAmount = 0;

        SaveAllPlayerData();
    }

    public void SaveDataAfterClosedGame()
    {
        SaveAllPlayerData();
    }

    public void SaveDataAfterShop()
    {
        SaveAllPlayerData();
    }

    private void SaveAllPlayerData()
    {
        SaveGameData();

        PlayerPrefs.SetInt(PlayerData.WALLET_AMOUNT_KEY, _playerData.WalletAmount);
        PlayerPrefs.SetInt(PlayerData.COLLECTED_COINS_AMOUNT_KEY, _playerData.CollectedCoinsAmount);
        PlayerPrefs.SetInt(PlayerData.BULLET_COUNT_KEY, _playerData.BulletCount);
        PlayerPrefs.SetFloat(PlayerData.FIRE_DAMAGE_KEY, _playerData.FireDamage);
        PlayerPrefs.SetFloat(PlayerData.FIRE_RATE_KEY, _playerData.FireRate);

        PlayerPrefs.SetInt(GameData.IMPROVEMENT_KEY, _gameData.ImprovementsUnlocked);

        PlayerPrefs.Save();
        Debug.Log("Данные игрока успешно сохранены.");
    }

    private void ResetGameData()
    {
        _gameData.CurrentLevel = 0;
        _gameData.CurrentStage = 1;
        _gameData.TotalStage = 5;
        _playerData.CollectedCoinsAmount = 0;

        SaveGameData();
    }
}
