using UnityEngine;
using UnityEngine.EventSystems;

namespace SupermarketSort
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private ItemType slotType;
        private bool _isFilled = false;

        public bool IsFilled => _isFilled;

        public void OnDrop(PointerEventData eventData)
        {
            var draggedItem = eventData.pointerDrag?.GetComponent<DragAndDrop>();
            if (draggedItem != null)
            {
                if (draggedItem.ItemType == slotType)
                {
                    draggedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    _isFilled = true; 
                    SSManager.Instance.CheckWinCondition();
                }
            }
        }
    }
}