using UnityEngine;

    public class BulletSpawner : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _defaultSpawn;
        [SerializeField] private Transform[] _bulletModifiedSpawns;

        [Header("Player Data | Scriptable Object")]
        [SerializeField] private PlayerData _playerData;

        [Header("Sound Settings")]
        [SerializeField] private SoundManager _soundManager;

        private float _nextFireTime = 0f;

        private void Update()
        {
            _nextFireTime -= Time.deltaTime;

            if (_nextFireTime <= 0)
            {
                Spawn();
                _nextFireTime = _playerData.FireRate;
            }
        }

        private void Spawn()
        {
            if (_playerData.BulletCount < 2)
            {
                SpawnBullet(_defaultSpawn);
            }
            else
            {
                foreach (Transform bulletSpawner in _bulletModifiedSpawns)
                {
                    SpawnBullet(bulletSpawner);
                }
            }
        }

        private void SpawnBullet(Transform bulletSpawner)
        {
            GameObject bullet = BulletPool.Instance.GetFromPool(bulletSpawner.position);
            _soundManager.ShootingClip();
        }
    }
