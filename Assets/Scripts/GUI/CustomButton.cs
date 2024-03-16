using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : Button, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _image;

    public UnityAction OnClicked;
    public UnityAction OnReleased;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        OnClicked?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        OnReleased?.Invoke();
    }
}
