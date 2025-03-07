using UnityEngine;
using UnityEngine.EventSystems;

namespace SupermarketSort
{
    public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [SerializeField] private Canvas canvas;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        
        [SerializeField] private ItemType itemType;
        public ItemType ItemType => itemType;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            
            if (canvas == null)
            {
                canvas = GetComponentInParent<Canvas>();
            }
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");    
            _rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _canvasGroup.blocksRaycasts = true;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
        }

        public void OnDrop(PointerEventData eventData)
        {
        }
    }
}