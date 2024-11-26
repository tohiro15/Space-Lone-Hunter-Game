using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    [SerializeField] private Player _player;
    public void ClickToReturn()
    {
        _player.CheckAndSaveHighScore();
        SceneManager.LoadScene(0);
    }
}
