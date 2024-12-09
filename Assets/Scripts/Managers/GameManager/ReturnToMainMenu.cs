using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerData _playerData;

    private PlayerDataManager _playerDM;
    private PlayerPrefsSystem _playerPS;

    private void Start()
    {
        _playerDM = _player.GetComponent<PlayerDataManager>();
        _playerPS = _player.GetComponent<PlayerPrefsSystem>();
    }
    public void ClickToReturn()
    {
        _playerDM.SavePlayerData();
        SceneManager.LoadScene("MainMenu");
    }
}
