using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SoundManager _soundManager;

    private float _health;
    private float _damage;
    private float _speed;

    private TextMeshProUGUI _healthText;

    private Transform _cachedTransform;

    private bool _hit;
    private bool _isActive;

    public event System.Action<Enemy> OnEnemyDestroyed;

    public void Initialize(SoundManager soundManager, float health, float damage, float speed)
    {
        _healthText = GetComponentInChildren<TextMeshProUGUI>();
        _healthText.text = health.ToString();

        _hit = false;
        _isActive = true; 

        _soundManager = soundManager;

        _cachedTransform = transform;

        _health = health;
        _damage = damage;
        _speed = speed;
    }
    private void Update()
    {
        if (!_isActive) return;

        if (_cachedTransform.position.y < -5f)
        {
            ReturnEnemyToPool();
            return;
        }

        _cachedTransform.position += Vector3.down * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D bullet)
    {
        if (!_isActive || !bullet.CompareTag("Bullet")) return;

        BulletPool.Instance.ReturnToPool(bullet.gameObject);
        if (!_hit)
        {
            _hit = true;
            DataManager.Instance.AddWallet(1);
        }

        TakeDamage(_damage);
        _soundManager.DestroyEnemyClip();
    }

    public void TakeDamage(float damage)
    {
        if (!_isActive || _health <= 0) return;

        _health -= damage;

        if (_health > 0)
        {
            _healthText.text = _health.ToString();
        }
        else
        {
            _healthText.text = "0";
            OnEnemyDestroyed?.Invoke(this);
            ReturnEnemyToPool();
        }
    }

    private void ReturnEnemyToPool()
    {
        if (!_isActive) return;

        _isActive = false;
        _hit = false;
        EnemyPool.Instance.ReturnToPool(gameObject);
    }
}
