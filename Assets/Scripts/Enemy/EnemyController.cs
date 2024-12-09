using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void Movement(float speed, GameObject enemy, EnemyPool enemyPool)
    {
        if (enemy.transform.position.y < -5f) enemyPool.ReturnEnemy(gameObject); ;

        Vector2 newPosition = enemy.transform.position;

        newPosition.y -= speed * Time.deltaTime;

        enemy.transform.position = newPosition;
    }

}
