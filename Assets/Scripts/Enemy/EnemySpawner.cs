using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float _spawnSpeed;

    [Header("Other")]
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private PlayerPrefsSystem _playerPS;
    [SerializeField] private SoundManager _soundManager;

    private bool _canSpawn = true;
    private void Update()
    {
        if(_canSpawn == true) StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        _canSpawn = false;

        GameObject enemy = _enemyPool.GetEnemy();

        yield return new WaitForSeconds(_spawnSpeed);

        _canSpawn = true;
    }
}
