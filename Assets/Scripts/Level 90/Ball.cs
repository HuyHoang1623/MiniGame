using UnityEngine;

public class Ball : MonoBehaviour
{
    public RectTransform arrowImage;
    public RectTransform ballUI;
    private RectTransform canvasRect;

    [SerializeField] private float maxLength = 200f;
    [SerializeField] private float shootPower = 10f;
    [SerializeField] private float friction = 0.95f;

    private bool isAiming = false;
    private Vector2 _startPos;
    private Vector2 _velocity;

    void Start()
    {
        if (canvasRect == null)
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
                canvasRect = canvas.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _velocity.magnitude < 0.1f)
        {
            isAiming = true;
            _startPos = Input.mousePosition;
            arrowImage.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0) && isAiming)
        {
            Vector2 currentMousePos = Input.mousePosition;
            Vector2 direction = (_startPos - currentMousePos).normalized;
            float distance = Mathf.Clamp(Vector2.Distance(currentMousePos, _startPos), 0f, maxLength);

            arrowImage.up = direction;
        }

        if (Input.GetMouseButtonUp(0) && isAiming)
        {
            isAiming = false;
            arrowImage.gameObject.SetActive(false);

            Vector2 direction = (_startPos - (Vector2)Input.mousePosition).normalized;
            float force = Mathf.Clamp(Vector2.Distance(_startPos, Input.mousePosition), 0f, maxLength) / maxLength;
            _velocity = direction * force * shootPower;
        }

        if (_velocity.magnitude > 0.1f)
        {
            ballUI.anchoredPosition += _velocity * Time.deltaTime * 60;
            CheckBounds();
            _velocity *= friction;
        }
        else
        {
            _velocity = Vector2.zero;
        }
    }

    void CheckBounds()
    {
        if (canvasRect == null) return;

        float canvasHalfWidth = canvasRect.rect.width / 2;
        float canvasHalfHeight = canvasRect.rect.height / 2;
        float ballHalfWidth = ballUI.rect.width / 2;
        float ballHalfHeight = ballUI.rect.height / 2;

        Vector2 ballPos = ballUI.anchoredPosition;

        if (ballPos.x - ballHalfWidth < -canvasHalfWidth)
        {
            _velocity.x *= -1; 
            ballPos.x = -canvasHalfWidth + ballHalfWidth;
        }
        else if (ballPos.x + ballHalfWidth > canvasHalfWidth)
        {
            _velocity.x *= -1;
            ballPos.x = canvasHalfWidth - ballHalfWidth;
        }

        if (ballPos.y - ballHalfHeight < -canvasHalfHeight)
        {
            _velocity.y *= -1;
            ballPos.y = -canvasHalfHeight + ballHalfHeight;
        }
        else if (ballPos.y + ballHalfHeight > canvasHalfHeight)
        {
            _velocity.y *= -1;
            ballPos.y = canvasHalfHeight - ballHalfHeight;
        }

        ballUI.anchoredPosition = ballPos;
    }


    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }

    public void HideBall()
    {
        ballUI.gameObject.SetActive(false);
    }
}
