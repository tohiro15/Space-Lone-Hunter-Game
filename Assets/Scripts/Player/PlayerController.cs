using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Movement(float speed, GameObject player, RectTransform controlZone)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (IsTouchInsideRectTransform(touch.position, controlZone))
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(
                    new Vector3(
                        touch.position.x,
                        touch.position.y,
                        Camera.main.transform.position.z - player.transform.position.z
                    )
                );

                Vector3 targetPosition = new Vector3(
                    touchPosition.x,
                    player.transform.position.y,
                    player.transform.position.z
                );

                player.transform.position = Vector3.Lerp(
                    player.transform.position,
                    targetPosition,
                    speed * Time.deltaTime
                );
            }
            limitCharacterMovement(player);
        }
    }

    private bool IsTouchInsideRectTransform(Vector2 screenPosition, RectTransform rectTransform)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            screenPosition,
            null, 
            out Vector2 localPoint
        );

        return rectTransform.rect.Contains(localPoint);
    }

    private void limitCharacterMovement(GameObject player)
    {
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float screenHeight = Camera.main.orthographicSize;

        if (_spriteRenderer != null)
        {
            float playerHalfWidth = _spriteRenderer.bounds.extents.x;
            float playerHalfHeight = _spriteRenderer.bounds.extents.y;

            Vector3 newPosition = player.transform.position;
            newPosition.x = Mathf.Clamp(newPosition.x, -screenWidth + playerHalfWidth, screenWidth - playerHalfWidth);
            newPosition.y = Mathf.Clamp(newPosition.y, -screenHeight + playerHalfHeight, screenHeight - playerHalfHeight);

            player.transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Player does not have a SpriteRenderer component!");
        }
    }
}
