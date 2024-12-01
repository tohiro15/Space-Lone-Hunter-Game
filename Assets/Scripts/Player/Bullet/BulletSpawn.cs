using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    private PlayerPrefsSystem _playerPS;

    private void Start()
    {
        _playerPS = GetComponentInParent<PlayerPrefsSystem>();
    }
    public void Spawn(GameObject bulletPrefab, Transform[] bulletSpawners)
    {
        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();
        bulletScript.Initialize(_playerPS);

        foreach (Transform bulletSpawner in bulletSpawners)
        {
            StartCoroutine(Spawner(bulletPrefab, bulletSpawner));
        }
    }

    IEnumerator Spawner(GameObject bulletPrefab, Transform bulletSpawner)
    {
        yield return new WaitForSeconds(1);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
    }
}
