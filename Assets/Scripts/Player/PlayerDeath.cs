using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Player _player;
    private PlayerDataManager _playerDataManager;
    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _playerDataManager = GetComponentInParent<PlayerDataManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
            _player.GameOverUI.SetActive(true);
            _player.ScoreUI.SetActive(false);
            _player.PauseButtonUI.SetActive(false);

            _player.CheckAndSaveHighScore();
            _playerDataManager.SavePlayerData();
        }
    }
}
