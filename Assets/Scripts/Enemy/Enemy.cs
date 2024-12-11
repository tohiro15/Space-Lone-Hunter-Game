using UnityEngine;

public class Enemy : MonoBehaviour
{
    private DataManager _dataManager;
    private SoundManager _soundManager;
    private int _health;
    private float _speed;

    private bool _hit;
    private bool _isActive;

    public event System.Action<Enemy> OnEnemyDestroyed;

    public void Initialize(DataManager DataManager, SoundManager soundManager, int health, float speed)
    {
        _hit = false;
        _isActive = true; 

        _dataManager = DataManager;
        _soundManager = soundManager;
        _health = health;
        _speed = speed;
    }

    private void Update()
    {
        if (!_isActive) return;

        if (transform.position.y < -5f) ReturnEnemyToPool();

        Vector2 newPosition = gameObject.transform.position;

        newPosition.y -= _speed * Time.deltaTime;

        gameObject.transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D bullet)
    {

        BulletPool.Instance.ReturnToPool(bullet.gameObject);

        if (!_isActive) return;
        if (bullet.CompareTag("Bullet"))
        {
            if (!_hit)
            {
                _hit = true;
                _dataManager.AddWallet(1);
            }

            TakeDamage(1);
            _soundManager.DestroyEnemyClip();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!_isActive) return;
        _health -= damage;

        if (_health <= 0)
        {
            OnEnemyDestroyed?.Invoke(this);
            ReturnEnemyToPool();
        }
    }

    private void ReturnEnemyToPool()
    {
        if (!_isActive) return;

        _isActive = false;
        EnemyPool.Instance.ReturnToPool(gameObject);
    }
}
