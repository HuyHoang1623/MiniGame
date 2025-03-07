using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour
{
    private float rotationSpeed = 180f;
    private float retractSpeed = 500f;
    private float extendSpeed = 500f;
    private float defaultRetractSpeed;
    private RectTransform canvasRect;

    private bool extending = false;
    private bool retracting = false;
    private Vector3 startPosition;
    private Transform target = null;
    private bool rotatingRight = true;
    private int pendingScore = 0; 

    void Start()
    {
        startPosition = transform.position;
        defaultRetractSpeed = retractSpeed;

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvasRect = canvas.GetComponent<RectTransform>();
            if (canvasRect == null)
            {
            }
        }
        else
        {
        }
    }

    void Update()
    {
        if (!extending && !retracting)
        {
            RotateHook();
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                extending = true;
            }
        }
        else if (extending)
        {
            MoveHook();
        }
        else if (retracting)
        {
            RetractHook();
        }
    }

    private void RotateHook()
    {
        float angle = transform.eulerAngles.z;
        if (angle > 180) angle -= 360;
        if (angle >= 60f) rotatingRight = false;
        if (angle <= -60f) rotatingRight = true;
        float rotationDirection = rotatingRight ? 1 : -1;
        transform.Rotate(0, 0, rotationSpeed * rotationDirection * Time.deltaTime);
    }

    private void MoveHook()
    {
        Vector3 newPosition = transform.position - transform.up * extendSpeed * Time.deltaTime;

        if (IsOutsideCanvas(newPosition))
        {
            extending = false;
            retracting = true;
        }
        else
        {
            transform.position = newPosition;
        }
    }

    private void RetractHook()
    {
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, startPosition, retractSpeed * Time.deltaTime);

        if (!IsOutsideCanvas(nextPosition))
        {
            transform.position = nextPosition;
        }

        if (target != null)
        {
            target.position = transform.position;
        }

        if (Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            retracting = false;
            extending = false;

            if (target != null)
            {
                Debug.Log("Kéo vật phẩm về thành công! Nhận được " + pendingScore + " điểm.");
                L103Manager.Instance.AddScore(pendingScore);
                target.gameObject.SetActive(false);
                target = null;
                pendingScore = 0;
            }

            retractSpeed = defaultRetractSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (target == null) 
        {
            MineItem item = other.GetComponent<MineItem>();
            if (item != null)
            {
                pendingScore = item.score; 
                retracting = true;
                extending = false;
                target = other.transform;

                float itemWeight = (item.weight > 0) ? item.weight : 1;
                retractSpeed = defaultRetractSpeed / itemWeight;
            }
        }
    }

    private bool IsOutsideCanvas(Vector3 worldPosition)
    {
        if (canvasRect == null)
        {
            return false; 
        }

        Vector3 localPosition = canvasRect.InverseTransformPoint(worldPosition);
        float halfWidth = canvasRect.rect.width / 2;
        float halfHeight = canvasRect.rect.height / 2;
        return (localPosition.x < -halfWidth || localPosition.x > halfWidth ||
                localPosition.y < -halfHeight || localPosition.y > halfHeight);
    }
}
