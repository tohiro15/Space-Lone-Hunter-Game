using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Statistic Settings")]
    [SerializeField] private float _speed;

    private int _numberBulletHits = 0;

    private EnemyController _controller;
    private PlayerPrefsSystem _playerPS;

    public delegate void EnemyDestroyed();
    public event EnemyDestroyed OnEnemyDestroyed;

    public void Initialize(PlayerPrefsSystem PlayerPrefsSystem)
    {
        _playerPS = PlayerPrefsSystem;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (_playerPS != null && _numberBulletHits < 1)
            {
                _playerPS.AddWallet(1);
                _numberBulletHits++;
            }
        }
    }
    private void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke();
    }
    void Start()
    {
        _controller = GetComponentInChildren<EnemyController>();
    }

    void Update()
    {
        _controller.Movement(_speed, gameObject);
    }
}
