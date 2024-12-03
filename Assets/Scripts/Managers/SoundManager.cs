using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;

    private int _currentClipIndex = 0;

    void Start()
    {
        if (_audioClips.Length > 0)
        {
            PlayClip(_currentClipIndex);
        }
    }
    public void PlayClip(int index)
    {
        if (index >= 0 && index < _audioClips.Length)
        {
            _audioSource.clip = _audioClips[index];
            _audioSource.Play();
            _currentClipIndex = index;
        }
        else
        {
            Debug.LogWarning("Индекс аудиоклипа вне массива!");
        }
    }

    public void StopClip()
    {
        _audioSource.Stop();
    }
}
