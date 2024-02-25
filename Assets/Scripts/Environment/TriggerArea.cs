using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerArea : MonoBehaviour
{
    [SerializeField] private LayerMask _interactionLayers;
    public UnityEvent OnAreaEnter;
    public UnityEvent OnAreaExit;

    private void OnTriggerEnter(Collider other)
    {
        if((1<<other.gameObject.layer & _interactionLayers) != 0)
        {
            OnAreaEnter?.Invoke();
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & _interactionLayers) != 0)
        {
            OnAreaExit?.Invoke();
        }
    }

}
