using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header ("Spawn Settings")]
    [SerializeField] private float _spawnSpeed;
    [SerializeField] private float _maxSpawnSpeed;

    [Header("Enemy Settings")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private PlayerPrefsSystem _playerPS;

    private bool _canSpawn = true;
    private void Update()
    {
        if(_canSpawn) StartCoroutine(Spawn());
    }
    public void IncreaseSpawnSpeed()
    {
        _spawnSpeed -= 0.01f;
    }

    IEnumerator Spawn()
    {
        _canSpawn = false;

        int randomPoint = Random.Range(0, _spawnPoints.Length);

        GameObject enemy = Instantiate(_enemyPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.Initialize(_playerPS);

        yield return new WaitForSeconds(_spawnSpeed);

        if (_spawnSpeed > _maxSpawnSpeed) enemyScript.OnEnemyDestroyed += IncreaseSpawnSpeed;

        _canSpawn = true;
    }
}
