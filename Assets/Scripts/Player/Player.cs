using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Data | Scriptable Object")]
    [SerializeField] private PlayerData _playerData;

    [Header("Bullet Settings")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] private Transform[] _bulletSpawners;
    [SerializeField] private float nextFireTime = 0f;

    private PlayerController _controller;
    private BulletSpawn _bulletSpawnerScript;
    private void Start()
    {
        _controller = GetComponentInChildren<PlayerController>();

        _bulletSpawnerScript = GetComponentInChildren<BulletSpawn>();
    }
    private void Update()
    {
        _controller.Movement(_playerData._speed, gameObject);
        UpdateNextFireTime();
    }

    private void UpdateNextFireTime()
    {
        if (Time.time >= nextFireTime)
        {
            _bulletSpawnerScript.Spawn(_bulletPrefab, _bulletSpawners);
            nextFireTime = Time.time + _playerData.FireRate;
        }
    }
}
