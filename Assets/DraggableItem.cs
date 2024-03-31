using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 initial;
    private Vector3 desiredPosition;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initial = rectTransform.position;
        desiredPosition = new Vector3(188.706787109375f, 1219.2515869140625f, 0f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // This method is called when the drag begins.
        // You can add any initialization logic here.
    }

    public void OnDrag(PointerEventData eventData)
    {
        // This method is called when the object is being dragged.

        if (rectTransform != null)
        {
            // Move the object based on the touch input position.
            rectTransform.anchoredPosition += eventData.delta;
            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (rectTransform != null)
        {
            // Check if the current position of the object is close to the desired position
            if (Vector3.Distance(rectTransform.position, desiredPosition) < 50f) // Adjust the threshold as needed
            {
                // If it is, destroy the object
                Destroy(gameObject);
                Debug.Log("Key Inserted !");
            }
            else
            {
                // If not, reset the position of the object
                rectTransform.position = initial;
            }
        }
    }
    

}


