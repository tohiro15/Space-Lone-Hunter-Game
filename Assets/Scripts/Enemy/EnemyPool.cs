using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _poolSize = 4;

    private Queue<GameObject> _enemyPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitializePool();
    }

    public bool HasAvailableEnemy()
    {
        return _enemyPool.Count > 0;
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.SetActive(false);
            _enemyPool.Enqueue(enemy);
        }
    }

    public GameObject GetFromPool(Vector3 position)
    {
        GameObject enemy;
        if (_enemyPool.Count > 0)
        {
            enemy = _enemyPool.Dequeue();
            if (enemy.activeSelf)
            {
                Debug.LogError("Получен активный объект из пула!");
                enemy.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Нет доступных врагов для спавна. Создаем нового...");
            enemy = CreateNewEnemy();
        }
            enemy.SetActive(true);
            enemy.transform.position = position;
            return enemy;
    }

    private GameObject CreateNewEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab);
        enemy.SetActive(false);
        return enemy;
    }

    public void ReturnToPool(GameObject enemy)
    {
        if (_enemyPool.Contains(enemy)) return;
        enemy.SetActive(false);
        _enemyPool.Enqueue(enemy);
    }
}
