using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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

        yield return new WaitForSeconds(1.5f);

        _canSpawn = true;
    }
}
