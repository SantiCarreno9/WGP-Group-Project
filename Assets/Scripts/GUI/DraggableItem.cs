using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public enum ItemCategory
    {
        Key
    }
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _rectTransform;
        private Vector3 _initialPosition;

        [SerializeField] private ItemCategory _category;
        public ItemCategory Category => _category;

        void Start()
        {
            _initialPosition = _rectTransform.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.position = _initialPosition;
            _image.raycastTarget = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

    }


}