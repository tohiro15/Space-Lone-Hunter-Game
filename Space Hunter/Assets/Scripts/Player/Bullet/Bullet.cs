using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Player _player;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);

            if (_player != null) _player.AddScore(1);
            else Debug.Log("The player is not initialized!");
        }
    }
    private void Update()
    {
        float newPosition = 5 * Time.deltaTime;
        gameObject.transform.position += new Vector3(0, newPosition, 0);

        if (gameObject.transform.position.y > 5.5f) Destroy(gameObject);
    }


    public void Initialize(Player player)
    {
        _player = player;
    }
}
