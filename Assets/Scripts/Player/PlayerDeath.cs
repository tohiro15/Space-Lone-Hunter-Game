using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    private PlayerPrefsSystem _playerPS;
    private PlayerDataManager _playerDM;
    private void Start()
    {
        _playerPS = GetComponentInParent<PlayerPrefsSystem>();
        _playerDM = GetComponentInParent<PlayerDataManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Time.timeScale = 0;

            _uiManager.UpdateGameOverUI();

            _uiManager.GameOverUI.SetActive(true);
            _uiManager.HUD.SetActive(false);
            _uiManager.PauseButton.SetActive(false);
            _playerDM.SavePlayerData();
        }
    }
}
