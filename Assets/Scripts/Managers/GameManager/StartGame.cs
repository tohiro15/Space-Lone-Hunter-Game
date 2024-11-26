using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void Starting()
    {
        SceneManager.LoadScene(1);
    }
}
