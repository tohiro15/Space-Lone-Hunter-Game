using UnityEngine;
using YG;

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
            if (YG2.envir.isMobile)
               _controller.HandleMobile(_playerData.Speed, gameObject, _controleZone);

            if (YG2.envir.isDesktop)
                _controller.HandleDesktop(_playerData.Speed, gameObject, _controleZone);
        }
    }
}
