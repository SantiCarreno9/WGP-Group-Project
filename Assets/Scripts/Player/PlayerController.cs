/* PlayerController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script performs receives all the inputs from the user and sends them to
 * the correct controller/module
 * 
 */

using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MovementController _movementController;
        [SerializeField] private AttackController _attackController;
        [SerializeField] private HealthModule _healthModule;

        private PlayerInputs _inputs;

        public Transform Transform => transform;
        public HealthModule HealthModule => _healthModule;

        private void Awake()
        {
            //Creates a new instance of the PlayerInputs class and subscribes to the events of the user inputs
            _inputs = new PlayerInputs();            

            #region PLAYER ACTION MAP

            _inputs.Player.Move.performed += Move_performed;
            _inputs.Player.Move.canceled += Move_canceled;
            _inputs.Player.Sprint.performed += Run_performed;
            _inputs.Player.Sprint.canceled += Run_canceled;

            _inputs.Player.Jump.performed += Jump_performed;

            _inputs.Player.Fire.performed += Fire_performed;
            _inputs.Player.Fire.canceled += Fire_canceled;
            #endregion

            _healthModule.OnDie += Die;
        }

        private void Start()
        {
            EnablePlayerActionMap();
        }

        private void OnEnable() => _inputs.Enable();

        private void OnDisable() => _inputs.Disable();

        #region USER INPUTS EVENTS

        #region MOVEMENT
        /// <summary>
        /// Reads the movement input and updates the avatar orientation
        /// </summary>
        /// <param name="obj"></param>
        private void Move_performed(InputAction.CallbackContext obj)
        {
            //if (GameManager.Instance.IsGamePaused())
            //    return;
            if (HealthModule.IsReceivingDamage)
                return;

            _movementController.SetUserMovementInput(obj.ReadValue<Vector2>());
        }

        /// <summary>
        /// Resets the movement Vector and updates the avatar orientation
        /// </summary>
        /// <param name="obj"></param>
        private void Move_canceled(InputAction.CallbackContext obj)
        {
            //if (GameManager.Instance.IsGamePaused())
            //    return;

            _movementController.SetUserMovementInput(Vector2.zero);
        }

        private void Run_performed(InputAction.CallbackContext obj)
        {
            //if (GameManager.Instance.IsGamePaused())
            //    return;
            if (HealthModule.IsReceivingDamage)
                return;

            _movementController.StartSprinting();
        }

        private void Run_canceled(InputAction.CallbackContext obj)
        {
            //if (GameManager.Instance.IsGamePaused())
            //    return;

            _movementController.StopSprinting();
        }

        private void Jump_performed(InputAction.CallbackContext obj)
        {
            //if (GameManager.Instance.IsGamePaused())
            //    return;

            if (HealthModule.IsReceivingDamage)
                return;

            _movementController.Jump();
        }

        #endregion

        #region ATTACK
        /// <summary>
        /// Ensures that the user is able to attack, if so it starts triggering the attack action
        /// </summary>
        /// <param name="obj"></param>
        private void Fire_performed(InputAction.CallbackContext obj)
        {
            //if (GameManager.Instance.IsGamePaused())
            //    return;

            if (_healthModule.IsReceivingDamage || _movementController.IsMovingBackwards())
                return;

            _attackController.StartAttacking();
        }

        /// <summary>
        /// Stops triggering the attack action
        /// </summary>
        /// <param name="obj"></param>
        private void Fire_canceled(InputAction.CallbackContext obj)
        {
            //if (GameManager.Instance.IsGamePaused())
            //    return;

            _attackController.StopAttacking();
        }

        #endregion        

        #endregion

        public void EnablePlayerActionMap() => _inputs.Player.Enable();
        public void DisablePlayerActionMap() => _inputs.Player.Disable();

        private void Die()
        {
            DisablePlayerActionMap();
        }
    }
}