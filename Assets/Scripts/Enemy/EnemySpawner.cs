using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float _spawnSpeed = 2f;

    [Header("Enemy Settings")]
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _maxEnemySpeed;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [Header("Content")]
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private SoundManager _soundManager;

    private bool _canSpawn = true;

    private void Start()
    {
        if(_playerData.StagePassed%5==0) _enemyData.Health += 1;

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (_canSpawn)
            {
                yield return new WaitForSeconds(_spawnSpeed);
                SpawnEnemy();
            }
            else
            {
                yield return null;
            }
        }
    }

    private void SpawnEnemy()
    {
        int randomPoint = Random.Range(0, _spawnPoints.Length);
        Vector3 spawnPosition = _spawnPoints[randomPoint].position;

        GameObject enemy = EnemyPool.Instance.GetFromPool(spawnPosition);


        if (enemy != null)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.OnEnemyDestroyed += OnEnemyDestroyed;

            float clampedSpeed = Mathf.Clamp(_enemySpeed, 0f, _maxEnemySpeed);
            enemyScript.Initialize(_dataManager, _soundManager, _enemyData.Health, clampedSpeed);
        }
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        _enemySpeed += 0.05f;

        _enemySpeed = Mathf.Clamp(_enemySpeed, 0f, _maxEnemySpeed);
    }
}
