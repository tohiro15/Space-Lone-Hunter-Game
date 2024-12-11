using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _poolSize = 4;

    private Queue<GameObject> _bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitializePool();
    }

    public bool HasAvailableBullet()
    {
        return _bulletPool.Count > 0;
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetFromPool(Vector3 position)
    {
        GameObject bullet;
        if (_bulletPool.Count > 0)
        {
            bullet = _bulletPool.Dequeue();
            if (bullet.activeSelf)
            {
                Debug.LogError("Получен активный объект из пула!");
                bullet.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Нет доступных пуль для спавна. Создаем новые...");
            bullet = CreateNewBullet();
        }
        bullet.SetActive(true);
        bullet.transform.position = position;
        return bullet;
    }

    private GameObject CreateNewBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.SetActive(false);
        return bullet;
    }

    public void ReturnToPool(GameObject bullet)
    {
        if (_bulletPool.Contains(bullet)) return;
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
