using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] Animator _blackout;

    [SerializeField] GameObject _baseCanvas;
    [SerializeField] GameObject _loadingCanvas;

    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TextMeshProUGUI _loadingText;

    private void Start()
    {
        Time.timeScale = 1;
    }
    #region Game
    public void StartGame()
    {
        StartCoroutine(AsyncLoadingScene("Game"));
    }
    #endregion
    #region MainMenu
    public void ReturnToMainMenu()
    {
        DataManager.Instance.SaveDataAfterClosedGame();
        StartCoroutine(AsyncLoadingScene("MainMenu"));
    }

    #endregion
    #region Shop Menu
    public void OpenShopMenu()
    {
        DataManager.Instance.SaveDataAfterClosedGame();
        StartCoroutine(AsyncLoadingScene("Shop"));
    }

    public void ClosedShopMenu(string sceneName)
    {
        StartCoroutine(AsyncLoadingScene(sceneName));
    }
    #endregion
    IEnumerator AsyncLoadingScene(string sceneName)
    {
        _blackout.enabled = true;
        yield return new WaitForSecondsRealtime(_blackout.speed);

        _baseCanvas.SetActive(false);
        _loadingCanvas.SetActive(true);

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
