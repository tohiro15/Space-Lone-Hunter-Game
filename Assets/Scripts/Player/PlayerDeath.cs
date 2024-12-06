using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SoundManager _soundManager;

    [SerializeField] private PlayerData _playerData;
    private PlayerDataManager _playerDM;
    private PlayerPrefsSystem _playerPS;
    private void Start()
    {
        _playerDM = GetComponentInParent<PlayerDataManager>();
        _playerPS = GetComponentInParent<PlayerPrefsSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Time.timeScale = 0;

            _soundManager.StopClip();

            _uiManager.UpdateGameOverUI();

            _playerDM.SavePlayerDataAfterDeath();
        }
    }
}
