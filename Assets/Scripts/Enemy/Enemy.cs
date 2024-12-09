using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health = 1;
    private float _speed = 3;

    private int _numberBulletHits = 0;

    private EnemyController _controller;
    private PlayerPrefsSystem _playerPS;
    private SoundManager _soundManager;

    public delegate void EnemyDestroyed();
    public event EnemyDestroyed OnEnemyDestroyed;



    public void Initialize(PlayerPrefsSystem PlayerPrefsSystem, SoundManager SoundManager,int health, float speed)
    {
        _playerPS = PlayerPrefsSystem;
        _soundManager = SoundManager;

        _health = health;
        _speed = speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (_playerPS != null && _numberBulletHits < 1)
            {
                _soundManager.DestroyEnemyClip();
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
