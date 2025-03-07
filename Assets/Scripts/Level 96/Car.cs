using UnityEngine;
using UnityEngine.EventSystems;

public class Car : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool isHorizontal; 
    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); 
    }

    public void OnBeginDrag(PointerEventData eventData) { }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 moveDirection = isHorizontal ?
            new Vector2(eventData.delta.x / canvas.scaleFactor, 0) :
            new Vector2(0, eventData.delta.y / canvas.scaleFactor);

        rectTransform.anchoredPosition += moveDirection;
    }

    public void OnEndDrag(PointerEventData eventData) { }
}