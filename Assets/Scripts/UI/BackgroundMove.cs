using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _bgSize;
    private float _bgPos;
    private void Start()
    {
        _bgSize = _spriteRenderer.bounds.size.y;
    }

    private void Update()
    {
        _bgPos -= _speed * Time.deltaTime;
        transform.position = new Vector3(0, Mathf.Repeat(_bgPos, _bgSize), 0);
    }
}
