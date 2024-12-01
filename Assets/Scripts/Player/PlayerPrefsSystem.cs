using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPrefsSystem : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlayerData _playerData;

    private Player _player;

    private int Collected—oinsAmount = 0;

    private void Start()
    {
        _player = GetComponent<Player>();
    }
    public void AddWallet(int value)
    {
        Collected—oinsAmount++;

        _playerData.WalletAmount += value;

        _uiManager.UpdateCoinUI(Collected—oinsAmount);
    }
}
