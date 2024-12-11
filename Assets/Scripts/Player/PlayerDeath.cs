using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private DataManager _dataManager;
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Time.timeScale = 0;

            _soundManager.StopClip();

            _uiManager.UpdateGameOverUI();

            _dataManager.SaveDataAfterDeath();
        }
    }
}
