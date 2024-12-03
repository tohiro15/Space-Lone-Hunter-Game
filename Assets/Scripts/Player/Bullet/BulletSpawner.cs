using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] private Transform[] _bulletSpawners;
    [SerializeField] private float nextFireTime = 0f;

    [Header("Player Data | Scriptable Object")]
    [SerializeField] private PlayerData _playerData;

    private PlayerPrefsSystem _playerPS;

    private void Start()
    {
        _playerPS = GetComponentInParent<PlayerPrefsSystem>();
    }

    private void Update()
    {
        UpdateNextFireTime();
    }
    public void Spawn(GameObject bulletPrefab, Transform[] bulletSpawners)
    {
        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();

        foreach (Transform bulletSpawner in bulletSpawners)
        {
            StartCoroutine(Spawner(bulletPrefab, bulletSpawner));
        }
    }
    private void UpdateNextFireTime()
    {
        if (Time.time >= nextFireTime)
        {
            Spawn(_bulletPrefab, _bulletSpawners);
            nextFireTime = Time.time + _playerData.FireRate;
        }
    }

    IEnumerator Spawner(GameObject bulletPrefab, Transform bulletSpawner)
    {
        yield return new WaitForSeconds(1);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
    }
}
