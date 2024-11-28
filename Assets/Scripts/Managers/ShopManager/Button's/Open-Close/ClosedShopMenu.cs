using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClosedShopMenu : MonoBehaviour
{
    [SerializeField] GameObject _shopCanvas;
    [SerializeField] GameObject _loadingCanvas;

    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TextMeshProUGUI _loadingText;
    public void CloseMenu()
    {
        _loadingCanvas.SetActive(true);

        StartCoroutine(AsyncLoadingGame());
    }

    IEnumerator AsyncLoadingGame()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");


        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            _loadingBar.value = progress;
            _loadingText.text = $"{(progress * 100):0}%";

            yield return null;
        }
    }
}
