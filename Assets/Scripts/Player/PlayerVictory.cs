using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVictory : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private PlayerData _playerData;
    private PlayerDataManager _playerDM;
    private void Start()
    {
        _playerDM = GetComponent<PlayerDataManager>();
    }
    public void Victory()
    {
        Time.timeScale = 0;

        _playerData.StagePassed++;
        _playerData.StageDuration += 15;

        if (_playerData.StagePassed > _playerData.RecordStage)
        {
            _playerData.RecordStage = _playerData.StagePassed;
        }

        _soundManager.VictoryClip();

        _uiManager.UpdateVictoryGameUI();

        _playerDM.SavePlayerData();
    }
}
