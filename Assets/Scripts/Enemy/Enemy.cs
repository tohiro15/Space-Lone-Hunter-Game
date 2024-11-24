using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Statistic Settings")]
    [SerializeField] private float _speed;

    private EnemyController _controller;

    void Start()
    {
        _controller = GetComponentInChildren<EnemyController>();
    }

    void Update()
    {
        _controller.Movement(_speed, gameObject);
    }
}
