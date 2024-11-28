using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _shopMenuCanvas;
    [SerializeField] private GameObject _gameCanvas;
    public void ClosedMenu()
    {
        _player.SetActive(false);
        _shopMenuCanvas.SetActive(false);
        _gameCanvas.SetActive(true);
    }
}
