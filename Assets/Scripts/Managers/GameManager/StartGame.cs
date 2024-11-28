using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class StartGame : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void Starting()
    {
        SceneManager.LoadScene("Game");
    }
}
