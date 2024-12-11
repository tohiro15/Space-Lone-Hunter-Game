using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    [SerializeField] private Player _player;
    private DataManager __dataManager;

    private void Start()
    {
        __dataManager = _player.GetComponent<DataManager>();
    }
    public void ClickToReturn()
    {
        __dataManager.SaveDataAfterDeath();
        SceneManager.LoadScene("MainMenu");
    }
}
