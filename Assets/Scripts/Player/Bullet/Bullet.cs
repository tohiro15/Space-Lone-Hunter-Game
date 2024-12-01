using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private PlayerPrefsSystem _playerPS;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            if (_playerPS != null)
            {
                _playerPS.AddWallet(1);
            }
            else Debug.Log("The player is not initialized!");
        }
    }
    private void Update()
    {
        float newPosition = 5 * Time.deltaTime;
        gameObject.transform.position += new Vector3(0, newPosition, 0);

        if (gameObject.transform.position.y > 5.5f) Destroy(gameObject);
    }


    public void Initialize(PlayerPrefsSystem PlayerPrefsSystem)
    {
        _playerPS = PlayerPrefsSystem;
    }
}
