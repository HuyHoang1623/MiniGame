using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public RectTransform objectUI; 
    public float speed = 200f;     
    private RectTransform canvasRect;
    private Vector2 direction = Vector2.left; 

    void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            canvasRect = canvas.GetComponent<RectTransform>();
        if (objectUI == null)
            objectUI = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (canvasRect == null || objectUI == null) return;

        objectUI.anchoredPosition += direction * speed * Time.deltaTime;

        CheckBounds();
    }

    void CheckBounds()
    {
        float canvasWidth = canvasRect.rect.width;
        float objWidth = objectUI.rect.width;

        Vector2 objPos = objectUI.anchoredPosition;

        float leftLimit = -canvasWidth / 2 + objWidth / 2;
        float rightLimit = canvasWidth / 2 - objWidth / 2;

        if (objPos.x <= leftLimit || objPos.x >= rightLimit)
        {
            direction *= -1; 
            objectUI.anchoredPosition = new Vector2(Mathf.Clamp(objPos.x, leftLimit, rightLimit), objPos.y);
            objectUI.localRotation *= Quaternion.Euler(0, 180, 0);
        }
    }

}