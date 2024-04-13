using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class Dropzone : MonoBehaviour, IDropHandler
    {
        [SerializeField] private ItemCategory _supportedDraggableCategory;
        public UnityAction OnItemDroppedOn;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out DraggableItem draggable))
            {
                if (draggable.Category == _supportedDraggableCategory)
                {
                    OnItemDroppedOn?.Invoke();
                    draggable.Hide();
                }                    
            }
        }
    }
}