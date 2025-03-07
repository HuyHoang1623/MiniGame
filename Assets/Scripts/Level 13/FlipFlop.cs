using System.Collections;
using UnityEngine;

namespace Level_13
{
    public class Flipflop : MonoBehaviour
    {
        public GameObject flipFlop;
        public Canvas canvas;

        private GameObject _currentFlipFlop;
        private bool _isHitting = false;
        private RectTransform _rectTransform;
        private Vector2 _previousPosition;
        public float minHittingVelocity = 0.001f;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (canvas == null)
            {
                canvas = FindObjectOfType<Canvas>();
            }

            if (canvas == null || flipFlop == null)
            {
                enabled = false;
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_isHitting)
            {
                StartCoroutine(HandleQuickHit());
            }

            if (_isHitting)
            {
                UpdateHit();
            }
        }

        void UpdateHit()
        {
            if (!_isHitting || canvas == null) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                Input.mousePosition,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            _rectTransform.anchoredPosition = localPoint;
            float velocity = (localPoint - _previousPosition).magnitude / Time.deltaTime;

            _previousPosition = localPoint;
        }

        void StartHitting()
        {
            if (_isHitting || flipFlop == null || canvas == null) return;

            _isHitting = true;
            _currentFlipFlop = Instantiate(flipFlop, canvas.transform);

            if (_currentFlipFlop.TryGetComponent(out RectTransform flipFlopRect))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.GetComponent<RectTransform>(),
                    Input.mousePosition,
                    canvas.worldCamera,
                    out Vector2 localPoint
                );
                flipFlopRect.anchoredPosition = localPoint;
                _previousPosition = localPoint;
            }
        }

        void StopHitting()
        {
            if (!_isHitting) return;

            _isHitting = false;

            if (_currentFlipFlop != null)
            {
                DestroyImmediate(_currentFlipFlop);
                _currentFlipFlop = null;
            }
        }

        private IEnumerator HandleQuickHit()
        {
            StartHitting();
            yield return new WaitForSeconds(0.1f);
            StopHitting();
        }

        private void OnDisable()
        {
            StopHitting();
        }
    }
}
