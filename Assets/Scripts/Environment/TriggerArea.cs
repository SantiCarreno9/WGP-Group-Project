using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerArea : MonoBehaviour
{
    [SerializeField] private LayerMask _interactionLayers;
    public UnityEvent OnAreaEnter;
    public UnityEvent OnAreaExit;
    private bool _objectOnArea = false;    

    private void OnDisable()
    {
        if (_objectOnArea)
            OnAreaExit?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;

        if ((1 << other.gameObject.layer & _interactionLayers) != 0)
        {
            OnAreaEnter?.Invoke();
            _objectOnArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!enabled) return;
        if ((1 << other.gameObject.layer & _interactionLayers) != 0)
        {
            OnAreaExit?.Invoke();
            _objectOnArea = false;
        }
    }

}
