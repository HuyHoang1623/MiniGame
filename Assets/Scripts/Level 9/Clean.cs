using UnityEngine;

public class Clean : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 spawnOffset = Vector3.zero;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            transform.position = worldPos;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Seed"))
        {
            Vector3 spawnPosition = collider.transform.position + (Vector3)spawnOffset;
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}