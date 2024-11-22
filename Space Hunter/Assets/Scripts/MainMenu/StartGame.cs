using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    public void Starting()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
