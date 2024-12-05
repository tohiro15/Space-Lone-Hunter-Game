using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _baseSource;
    [SerializeField] private AudioSource _playerSource;
    [SerializeField] private AudioSource _enemySource;

    [SerializeField] private AudioClip[] _music;
    [SerializeField] private AudioClip _shooting;
    [SerializeField] private AudioClip _destroyEnemy;

    private int _currentClipIndex = 0;

    void Start()
    {
        _baseSource = GetComponent<AudioSource>();
        if (_music.Length > 0)
        {
            PlayClip(_currentClipIndex);
        }
    }
    public void PlayClip(int index)
    {
        if (index >= 0 && index < _music.Length)
        {
            _baseSource.clip = _music[index];
            _baseSource.Play();
            _currentClipIndex = index;
        }
        else
        {
            Debug.LogWarning("Индекс аудиоклипа вне массива!");
        }
    }
    public void ShootingClip()
    {
        _playerSource.PlayOneShot(_shooting);
    }

    public void DestroyEnemyClip()
    {
        _enemySource.PlayOneShot(_destroyEnemy);
    }

    public void StopClip()
    {
        _baseSource.Stop();
    }
}
