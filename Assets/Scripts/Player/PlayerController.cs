using UnityEngine;
using YG;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Camera _cam;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _cam = Camera.main;
    }

    private void Update()
    {
        ClampToScreen();
    }

    public void HandleDesktop(float speed, GameObject player, RectTransform controlZone)
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector3 movement = new Vector3(moveX, 0f, 0f);
        transform.position += movement * speed * Time.deltaTime;
    }
    public void HandleMobile(float speed, GameObject player, RectTransform controlZone)
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (!IsTouchInsideRectTransform(touch.position, controlZone))
            return;

        Vector3 worldPoint = _cam.ScreenToWorldPoint(
            new Vector3(
                touch.position.x,
                touch.position.y,
                _cam.transform.position.z * -1f
            )
        );

        Vector3 target = new Vector3(worldPoint.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }
    private void ClampToScreen()
    {
        float screenHeight = _cam.orthographicSize;
        float screenWidth = screenHeight * _cam.aspect;

        float halfWidth = _spriteRenderer.bounds.extents.x;
        float halfHeight = _spriteRenderer.bounds.extents.y;

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -screenWidth + halfWidth, screenWidth - halfWidth);
        pos.y = Mathf.Clamp(pos.y, -screenHeight + halfHeight, screenHeight - halfHeight);

        transform.position = pos;
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
}
