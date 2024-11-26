using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenuUI;
    [SerializeField] private GameObject _pauseButton;
    public void ClosedMenu()
    {
        Time.timeScale = 1;
        _shopMenuUI.SetActive(false);
        _pauseButton.SetActive(true);
    }
}
