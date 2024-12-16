using UnityEngine;

public class PlayerVictory : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private GameData _gameData;
    public void Victory()
    {
        Time.timeScale = 0;

        _soundManager.VictoryClip();

        PlayerPrefs.SetInt(GameData.STAGE_PASSED_KEY, _gameData.StagePassed += 1);

        _uiManager.UpdateVictoryGameUI();

        DataManager.Instance.SaveDataAfterVictory();
    }
}
