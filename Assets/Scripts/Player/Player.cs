using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RectTransform _controleZone;

    [Header("Player Data | Scriptable Object")]
    [SerializeField] private PlayerData _playerData;

    private PlayerController _controller;

    private void Start()
    {
        _controller = GetComponentInChildren<PlayerController>();
    }

    private void Update()
    {
        if (_controller != null && _playerData != null && _playerData.Speed > 0)
        {
            _controller.Movement(_playerData.Speed, gameObject, _controleZone);
        }
    }
}
