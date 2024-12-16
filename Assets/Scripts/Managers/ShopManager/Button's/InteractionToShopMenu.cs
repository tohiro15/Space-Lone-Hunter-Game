using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class InteractionToShopMenu : MonoBehaviour
{
    [SerializeField] GameObject _baseCanvas;
    [SerializeField] GameObject _loadingCanvas;

    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TextMeshProUGUI _loadingText;
    public void OpenShopMenu()
    {
        DataManager.Instance.SaveDataAfterVictory();
        _loadingCanvas.SetActive(true);

        StartCoroutine(AsyncLoadingScene("Shop"));
    }

    public void ClosedShopMenu(string sceneName)
    {
        _loadingCanvas.SetActive(true);

        StartCoroutine(AsyncLoadingScene(sceneName));
    }


    IEnumerator AsyncLoadingScene(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);


        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            _loadingBar.value = progress;
            _loadingText.text = $"{(progress * 100):0}%";

            yield return null;
        }
    }
}
