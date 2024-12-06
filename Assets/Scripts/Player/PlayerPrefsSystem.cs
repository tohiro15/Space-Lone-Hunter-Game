using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPrefsSystem : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlayerData _playerData;

    private Player _player;


    private void Start()
    {
        _player = GetComponent<Player>();
    }
    public void AddWallet(int value)
    {
        _playerData.Collected—oinsAmount++;

        _playerData.WalletAmount += value;
    }
}
