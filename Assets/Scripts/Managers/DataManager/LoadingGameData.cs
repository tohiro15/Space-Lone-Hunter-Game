using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingGameData : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TextMeshProUGUI _loadingText;
    private void Start()
    {
        StartCoroutine(LoadingAsync());
    }

    IEnumerator LoadingAsync()
    {
        AsyncOperation loadingDataOperation = SceneManager.LoadSceneAsync("MainMenu");

        while (!loadingDataOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadingDataOperation.progress / 0.9f);

            _loadingBar.value = progress;
            _loadingText.text = $"{(progress * 100):0}%";

            yield return null;
        }
    }
}