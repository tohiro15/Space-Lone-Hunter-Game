using UnityEngine;

public class PlayResult : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private UIManager _uiManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
            _soundManager.GameoverClip();
            _uiManager.UpdateGameOverUI();
            DataManager.Instance.SaveDataAfterDeath();
        }
    }
    public void Victory()
    {
        Time.timeScale = 0;
        _soundManager.VictoryClip();
        _uiManager.UpdateVictoryGameUI();
        DataManager.Instance.SaveDataAfterVictory();
    }
}
