using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class OpenShopMenu : MonoBehaviour
{
    [SerializeField] GameObject _gameCanvas;
    [SerializeField] GameObject _loadingCanvas;

    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TextMeshProUGUI _loadingText;
    public void OpenMenu()
    {
        _loadingCanvas.SetActive(true);

        StartCoroutine(AsyncLoadingShop());
    }

    IEnumerator AsyncLoadingShop()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Shop");


        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            _loadingBar.value = progress;
            _loadingText.text = $"{(progress * 100):0}%";

            yield return null;
        }
    }
}
