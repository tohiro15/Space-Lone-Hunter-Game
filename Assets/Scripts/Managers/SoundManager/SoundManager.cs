using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _baseSource;
    [SerializeField] private AudioSource _playerSource;
    [SerializeField] private AudioSource _enemySource;

    [SerializeField] private AudioClip[] _music;

    [Header("Player Sounds Effects")]
    [SerializeField] private AudioClip _victorySound;
    [SerializeField] private AudioClip _gameoverSound;
    [SerializeField] private AudioClip _shooting;

    [Header("Enemy Sounds Effects")]
    [SerializeField] private AudioClip _destroyEnemy;

    private int _currentClipIndex;

    void Start()
    {
        if (_music == null || _music.Length == 0)
        {
            Debug.LogWarning("Ќет музыкальных треков дл€ воспроизведени€.");
            return;
        }

        PlayClip(_currentClipIndex);
    }
    public void PlayClip(int index)
    {
        if (_music == null || _music.Length == 0)
        {
            Debug.LogWarning("ћассив аудиотреков пуст!");
            return;
        }

        index = Mathf.Clamp(index, 0, _music.Length - 1);
        if (_baseSource.clip != _music[index])
        {
            _baseSource.clip = _music[index];
            _baseSource.Play();
            _currentClipIndex = index;
        }
    }
    private void PlaySound(AudioSource source, AudioClip clip, bool stopCurrent = false)
    {
        if (clip == null)
        {
            Debug.LogWarning("ѕопытка воспроизвести пустой аудиоклип!");
            return;
        }

        if (stopCurrent)
            source.Stop();

        source.PlayOneShot(clip);
    }

    public void ShootingClip()
    {
        PlaySound(_playerSource, _shooting);
    }

    public void DestroyEnemyClip()
    {
        PlaySound(_enemySource, _destroyEnemy);
    }

    public void VictoryClip()
    {
        PlaySound(_baseSource, _victorySound, true);
    }
    public void GameoverClip()
    {
        PlaySound(_baseSource, _gameoverSound, true);
    }
    public void StopClip()
    {
        _baseSource.Stop();
    }
}
