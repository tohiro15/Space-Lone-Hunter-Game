using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    [SerializeField] private Player _player;
    public void ClickToReturn()
    {
        DataManager.Instance.SaveDataAfterDeath();
        SceneManager.LoadScene("MainMenu");
    }
}
