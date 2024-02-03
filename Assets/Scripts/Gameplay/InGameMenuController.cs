using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuController : MonoBehaviour
{
    private PlayerInputs _inputs;
    // Start is called before the first frame update
    void Awake()
    {
        _inputs = new PlayerInputs();
        _inputs.Menu.Pause.performed += Pause_performed;
        _inputs.Menu.Inventory.performed += Inventory_performed;
    }

    private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Time.timeScale = (Time.timeScale != 0) ? 0 : 1;
    }

    private void OnEnable() => _inputs.Enable();
    
    private void OnDisable() => _inputs.Disable();    
    
}
