using System.Collections;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }
    public void Spawn(GameObject bulletPrefab)
    {
        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();
        bulletScript.Initialize(_player);

        StartCoroutine(Spawner(bulletPrefab));
    }

    IEnumerator Spawner(GameObject bulletPrefab)
    {
        yield return new WaitForSeconds(1);
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
