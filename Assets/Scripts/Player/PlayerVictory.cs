using UnityEngine;

public class PlayerVictory : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private PlayerData _playerData;
    [SerializeField] DataManager _dataManager;
    private void Start()
    {
        _dataManager = GetComponent<DataManager>();
    }
    public void Victory()
    {
        Time.timeScale = 0;

        _soundManager.VictoryClip();

        _uiManager.UpdateVictoryGameUI();

        if (_playerData.StagePassed > _playerData.RecordStage)
        {
            _playerData.RecordStage = _playerData.StagePassed;
        }

        _playerData.StagePassed++;

        _dataManager.SaveDataAfterVictory();
    }
}
