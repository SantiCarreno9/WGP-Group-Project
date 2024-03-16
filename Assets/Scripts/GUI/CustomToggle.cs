using UnityEngine.UI;
using UnityEngine.Events;

public class CustomToggle : Toggle
{
    public UnityEvent OnActivated;
    public UnityEvent OnDeactivated;

    protected override void Start()
    {
        onValueChanged.AddListener(TriggerEvents);
    }

    private void TriggerEvents(bool value)
    {        
        if (value) OnActivated?.Invoke();
        else OnDeactivated?.Invoke();
    }
}
