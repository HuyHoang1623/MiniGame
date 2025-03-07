using UnityEngine;

namespace Level_13
{
    public class MouseMovement : MonoBehaviour
    {
        public float speed = 500f;
        private Vector2 _direction;

        private RectTransform _rectTransform;
        private RectTransform _canvasRect;

        private bool _isBouncingX = false;
        private bool _isBouncingY = false;

        public GameObject DeadMouse;
        public Transform folder;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            _direction = Random.insideUnitCircle.normalized;

            UpdateRotation();
            
            HMManager.instance.RegisterMouse();
        }

        void Update()
        {
            _rectTransform.anchoredPosition += _direction * speed * Time.deltaTime;

            Vector2 pos = _rectTransform.anchoredPosition;
            Vector2 canvasSize = _canvasRect.sizeDelta;

            if (pos.x <= -canvasSize.x / 2 || pos.x >= canvasSize.x / 2)
            {
                if (!_isBouncingX)
                {
                    _direction.x = -_direction.x;
                    _isBouncingX = true;
                    UpdateRotation();
                }
            }
            else
            {
                _isBouncingX = false;
            }

            if (pos.y <= -canvasSize.y / 2 || pos.y >= canvasSize.y / 2)
            {
                if (!_isBouncingY)
                {
                    _direction.y = -_direction.y;
                    _isBouncingY = true;
                    UpdateRotation();
                }
            }
            else
            {
                _isBouncingY = false;
            }
        }

        void UpdateRotation()
        {
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            _rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("FlipFlop"))
            {
                GameObject deadMouse = Instantiate(DeadMouse, transform.position, _rectTransform.rotation);

                if (folder != null)
                {
                    deadMouse.transform.SetParent(folder, true);
                }

                gameObject.SetActive(false);
                
                HMManager.instance.UnregisterMouse();
            }
        }
    }
}