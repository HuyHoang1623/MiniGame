using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float speed = 300f;
    private RectTransform _rectTransform;
    private bool _moveRight = true;
    private float leftBound, rightBound;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        RectTransform canvasRect = transform.parent?.GetComponent<RectTransform>();
        if (canvasRect == null)
        {
            Debug.LogError("Item không nằm trong Canvas!");
            return;
        }

        leftBound = -canvasRect.rect.width / 2 + _rectTransform.rect.width / 2;
        rightBound = canvasRect.rect.width / 2 - _rectTransform.rect.width / 2;

        // Thông báo cho L83Manager về số lượng item hiện có
        L83Manager.Instance?.RegisterItem();
    }

    private void FixedUpdate()
    {
        MoveSmooth();
    }

    private void MoveSmooth()
    {
        if (_rectTransform == null) return;

        float moveAmount = speed * Time.fixedDeltaTime;
        Vector2 newPosition = _rectTransform.anchoredPosition;
        newPosition.x += _moveRight ? moveAmount : -moveAmount;

        _rectTransform.anchoredPosition = newPosition;

        if (newPosition.x >= rightBound) _moveRight = false;
        else if (newPosition.x <= leftBound) _moveRight = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            L83Manager.Instance?.OnItemDestroyed();
            Destroy(gameObject);
        }
    }
}