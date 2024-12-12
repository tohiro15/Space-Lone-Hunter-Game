using UnityEngine;

public class PlayerVictory : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private PlayerData _playerData;
    public void Victory()
    {
        Time.timeScale = 0;

        _soundManager.VictoryClip();

        _uiManager.UpdateVictoryGameUI();

        DataManager.Instance.SaveDataAfterVictory();
    }
}
