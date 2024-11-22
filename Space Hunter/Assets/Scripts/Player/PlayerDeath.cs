using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private Player _player;
    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
           _gameOverUI.SetActive(true);
            _player.CheckAndSaveHighScore();
        }
    }
}
