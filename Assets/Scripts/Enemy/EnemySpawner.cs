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

    private bool _canSpawn = true;
    private void Update()
    {
        if(_canSpawn) StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        _canSpawn = false;

        int randomPoint = Random.Range(0, _spawnPoints.Length);

        Instantiate(_enemyPrefab, _spawnPoints[randomPoint]);

        yield return new WaitForSeconds(_spawnSpeed);

        if(_spawnSpeed > _maxSpawnSpeed ) _spawnSpeed -= 0.001f;

        _canSpawn = true;
    }
}
