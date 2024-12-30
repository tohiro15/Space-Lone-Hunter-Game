using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Range(1.5f, 2f)]
    [SerializeField] private float _spawnSpeed = 2f;
    private float _maxSpawnSpeed = 1.5f;

    [Header("Enemy Settings")]
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _maxEnemySpeed;
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Content")]
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameData _gameData;

    private bool _canSpawn = true;

    private void Start()
    {
        _enemyData.Health = Mathf.Clamp(_gameData.CurrentStage, 1, _enemyData.MaxHealth);
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
        Vector3 screenMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Vector3 spawnPosition = Vector3.zero;
        spawnPosition = new Vector3(Random.Range(screenMin.x, screenMax.x), screenMax.y + 1, 0);

        GameObject enemy = EnemyPool.Instance.GetFromPool(spawnPosition);

        if (enemy != null)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.OnEnemyDestroyed += OnEnemyDestroyed;
            float clampedEnemySpeed = Mathf.Clamp(_enemySpeed, 0f, _maxEnemySpeed);
            enemyScript.Initialize(_soundManager, _enemyData.Health, _playerData.FireDamage, clampedEnemySpeed);
        }
    }


    private void OnEnemyDestroyed(Enemy enemy)
    {
        if (_spawnSpeed > _maxSpawnSpeed)
        {
            _spawnSpeed -= 0.05f;
        }

        _enemySpeed += 0.05f;
        _enemySpeed = Mathf.Clamp(_enemySpeed, 0f, _maxEnemySpeed);

    }

}