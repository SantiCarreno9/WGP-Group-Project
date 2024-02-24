/* InGameMenuController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script opens the proper UI according to the user's inputs
 * 
 */
using Character;
using UnityEngine;

public class InGameMenuController : MonoBehaviour
{
    private PlayerInputs _inputs;
    [SerializeField] private InGameUIManager _inGameUIManager;
    private PlayerController _controller;

    private bool _isInventoryOpen = false;

    void Awake()
    {
        //Creates a new instance of the PlayerInputs and subscribes to its actions
        _inputs = new PlayerInputs();
        _inputs.Menu.Pause.performed += Pause_performed;
        _inputs.Menu.Inventory.performed += Inventory_performed;
    }

    private void Start()
    {
        _controller = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// Shows or hides the inventory and pauses the game
    /// </summary>
    /// <param name="obj"></param>
    private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isInventoryOpen = !_isInventoryOpen;
        if (_isInventoryOpen)
        {
            PauseGame();
            _inGameUIManager.openInventoryCanvas();
        }
        else
        {
            _inGameUIManager.closeInventoryCanvas();
            ResumeGame();
        }
    }

    /// <summary>
    /// Pauses or resumes the game
    /// </summary>
    /// <param name="obj"></param>
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        bool isGamePaused = (Time.timeScale == 0);
        if (!isGamePaused) PauseGame();
        else ResumeGame();
    }

    /// <summary>
    /// Resumes the game and closes the pause menu
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _inGameUIManager.closePauseCanvas();
        if (_controller != null)
            _controller.EnablePlayerActionMap();
    }

    /// <summary>
    /// Opens the pause menu and pauses the game
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        _inGameUIManager.displayPauseCanvas();
        if (_controller != null)
            _controller.DisablePlayerActionMap();
    }

    private void OnEnable() => _inputs.Enable();

    private void OnDisable() => _inputs.Disable();

}
