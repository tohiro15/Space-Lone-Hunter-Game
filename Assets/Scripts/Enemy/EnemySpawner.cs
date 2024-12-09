using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float _spawnSpeed;

    [Header("Enemy Settings")]
    [SerializeField] private int _enemyHealth;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyMaxSpeed;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [Header("Other")]
    [SerializeField] private PlayerPrefsSystem _playerPS;
    [SerializeField] private SoundManager _soundManager;


    private bool _canSpawn = true;
    private void Update()
    {
        foreach (Transform point in _spawnPoints)
        {
            if (_canSpawn) StartCoroutine(Spawn());
        }
    }
    public void IncreaseSpeed()
    {
        _enemySpeed += 0.1f;
    }

    IEnumerator Spawn()
    {
        _canSpawn = false;

        int randomPoint = Random.Range(0, _spawnPoints.Length);

        GameObject enemy = Instantiate(_enemyPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.Initialize(_playerPS, _soundManager, _enemyHealth, _enemySpeed);

        yield return new WaitForSeconds(_spawnSpeed);

        if (_enemySpeed < _enemyMaxSpeed)
        {
            enemyScript.OnEnemyDestroyed += IncreaseSpeed;
        }

        _canSpawn = true;
    }
}
