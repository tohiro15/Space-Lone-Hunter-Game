using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        BulletMovement();
    }

    private void BulletMovement()
    {
        float newPosition = 5 * Time.deltaTime;
        gameObject.transform.position += new Vector3(0, newPosition, 0);

        if (gameObject.transform.position.y > 5.5f) BulletPool.Instance.ReturnToPool(gameObject);
    }
}
