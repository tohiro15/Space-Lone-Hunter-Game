using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _bgSize;
    private float _bgPos;
    private void Start()
    {
        _bgSize = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        _bgPos -= _speed * Time.deltaTime;
        _bgPos = Mathf.Repeat(_bgPos, _bgSize);
        transform.position = new Vector3(0, _bgPos, 0);
    }
}
