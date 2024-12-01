using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    [SerializeField] private Player _player;

    private PlayerPrefsSystem _playerPS;
    private PlayerDataManager _playerDM;

    private void Start()
    {
        _playerPS = _player.GetComponent<PlayerPrefsSystem>();
        _playerDM = _player.GetComponent<PlayerDataManager>();
    }
    public void ClickToReturn()
    {
        _playerDM.SavePlayerData();
        SceneManager.LoadScene("MainMenu");
    }
}
