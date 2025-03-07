using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileOffsetY = 50f;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private float _shootCooldown = 1f; 

    private AudioSource _audioSource;
    private RectTransform _rectTransform;
    private Vector2 _targetPosition;
    private bool _isShooting = false;
    private bool _isTouching = false;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        _targetPosition = _rectTransform.anchoredPosition;
    }

    private void Update()
    {
        HandleTouchInput();
    }

    private void FixedUpdate()
    {
        MoveRocket();
    }

    private void MoveRocket()
    {
        _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _targetPosition, _speed * Time.fixedDeltaTime);
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                touch.position,
                null,
                out _targetPosition
            );

            if (touch.phase == TouchPhase.Began)
            {
                _isTouching = true;
                StartShooting();
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isTouching = false;
            }
        }
    }

    private void StartShooting()
    {
        if (!_isShooting)
        {
            StartCoroutine(ShootContinuously());
        }
    }

    private IEnumerator ShootContinuously()
    {
        _isShooting = true;
        while (_isTouching)
        {
            Shoot();
            yield return new WaitForSeconds(_shootCooldown);
        }
        _isShooting = false;
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(_projectilePrefab, transform.parent.parent, false);
        projectile.GetComponent<RectTransform>().anchoredPosition = _rectTransform.anchoredPosition + new Vector2(0, _projectileOffsetY);

        if (_shootSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(_shootSound);
        }
    }
}
