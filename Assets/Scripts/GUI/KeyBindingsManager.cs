using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBindingsManager : MonoBehaviour
{
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _jump;
    [SerializeField] private InputActionReference _run;
    [SerializeField] private InputActionReference _fire;
    [SerializeField] private InputActionReference _inventory;

    private void OnEnable()
    {
        _move.action.Disable();
        _jump.action.Disable();
        _run.action.Disable();
        _fire.action.Disable();
        _inventory.action.Disable();
    }

    private void OnDisable()
    {
        _move.action.Enable();
        _jump.action.Enable();
        _run.action.Enable();
        _fire.action.Enable();
        _inventory.action.Enable();
    }

}
