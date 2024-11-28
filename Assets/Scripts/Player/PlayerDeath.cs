using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Player _player;
    private void Start()
    {
        _player = GetComponentInParent<Player>();
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
            _player.SaveWalletAmount();
        }
    }
}
