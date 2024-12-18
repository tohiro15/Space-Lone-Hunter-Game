using System.Collections;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsCounter;
    private float _fps = 0.0f;

    void Start()
    {
        StartCoroutine(FPSCounterCoroutine());
    }

    private IEnumerator FPSCounterCoroutine()
    {
        while (true)
        {
            _fps = 1.0f / Time.unscaledDeltaTime;
            _fpsCounter.text = $"FPS: {_fps:0.}";
            yield return new WaitForSeconds(1f);
        }
    }
}
