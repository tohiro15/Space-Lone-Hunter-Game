using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // Настройки врагов и пул
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyMaxSpeed;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _initialSize = 10;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private List<GameObject> _enemyPool = new List<GameObject>();
    [SerializeField] private PlayerPrefsSystem _playerPS;
    [SerializeField] private SoundManager _soundManager;

    private void Start()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            CreateNewEnemy();
        }
    }

    public void IncreaseSpeed()
    {
        _enemySpeed += 0.1f;
    }

    public GameObject GetEnemy()
    {
        GameObject enemy = _enemyPool.Find(o => !o.activeInHierarchy);

        if (enemy != null)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            enemy.transform.position = _spawnPoints[randomIndex].position;

            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            return CreateNewEnemy();
        }
    }

    public void ReturnEnemy(GameObject enemy)
    {
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.ResetHit();  // Сбрасываем флаг попадания

        enemy.SetActive(false);

        if (_enemySpeed < _enemyMaxSpeed)
        {
            IncreaseSpeed();

            enemyScript.Initialize(_enemySpeed, _playerPS, _soundManager, gameObject);
        }

        if (!_enemyPool.Contains(enemy))
        {
            _enemyPool.Add(enemy);
        }
    }

    private GameObject CreateNewEnemy()
    {
        var obj = Instantiate(_enemyPrefab);

        Enemy enemyScript = obj.GetComponent<Enemy>();
        enemyScript.Initialize(_enemySpeed, _playerPS, _soundManager, gameObject);

        obj.SetActive(false);
        _enemyPool.Add(obj);
        return obj;
    }
}
