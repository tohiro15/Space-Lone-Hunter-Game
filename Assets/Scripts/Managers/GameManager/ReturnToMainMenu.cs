using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    [SerializeField] private Player _player;
    private PlayerDataManager _playerDataManager;

    private void Start()
    {
        _playerDataManager = _player.GetComponent<PlayerDataManager>();
    }
    public void ClickToReturn()
    {
        _player.CheckAndSaveHighScore();
        _playerDataManager.SavePlayerData();
        SceneManager.LoadScene("MainMenu");
    }
}
