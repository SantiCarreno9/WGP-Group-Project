using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _pressedColor = Color.white;

    public UnityAction OnClicked;
    public UnityAction OnReleased;

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.color = _pressedColor;
        OnClicked?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.color = _normalColor;
        OnReleased?.Invoke();
    }
}
