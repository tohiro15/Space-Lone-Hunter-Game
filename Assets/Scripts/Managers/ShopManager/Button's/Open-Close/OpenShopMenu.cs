using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenuUI;
    [SerializeField] private GameObject _pauseButton;

    private void Start()
    {
        _shopMenuUI.SetActive(false);
    }
    public void OpenMenu()
    {
        Time.timeScale = 0;
        _shopMenuUI.SetActive(true);
        _pauseButton.SetActive(false);
    }
}
