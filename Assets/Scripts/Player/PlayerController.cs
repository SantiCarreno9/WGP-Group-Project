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

            _inputs.Player.Attack.performed += Attack_performed;
            _inputs.Player.Attack.canceled += Attack_canceled;
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
        private void Move_performed(InputAction.CallbackContext obj) => HandleMovementInputPerformed(obj.ReadValue<Vector2>());
        
        private void Move_canceled(InputAction.CallbackContext obj) => HandleMovementInputCanceled();

        private void Run_performed(InputAction.CallbackContext obj) => HandleSprintInputPerformed();

        private void Run_canceled(InputAction.CallbackContext obj) => HandleSprintInputCanceled();

        private void Jump_performed(InputAction.CallbackContext obj) => HandleJumpInputPerformed();

        #endregion

        #region ATTACK        
        private void Attack_performed(InputAction.CallbackContext obj) => HandleAttackInputPerformed();
        
        private void Attack_canceled(InputAction.CallbackContext obj) => HandleAttackInputCanceled();

        #endregion        

        #endregion

        public void EnablePlayerActionMap() => _inputs.Player.Enable();
        public void DisablePlayerActionMap() => _inputs.Player.Disable();

        #region PUBLIC METHODS

        #region MOVEMENT
        /// <summary>
        /// Reads the movement input and updates the avatar orientation
        /// </summary>
        /// <param name="obj"></param>
        public void HandleMovementInputPerformed(Vector2 value)
        {
            if (HealthModule.IsReceivingDamage)
                return;

            _movementController.SetUserMovementInput(value);
        }

        /// <summary>
        /// Resets the movement Vector and updates the avatar orientation
        /// </summary>
        /// <param name="obj"></param>
        public void HandleMovementInputCanceled()
        {
            _movementController.SetUserMovementInput(Vector2.zero);
        }

        public void HandleSprintInputPerformed()
        {
            if (HealthModule.IsReceivingDamage)
                return;

            _movementController.StartSprinting();
        }

        public void HandleSprintInputCanceled()
        {
            _movementController.StopSprinting();
        }

        public void HandleJumpInputPerformed()
        {
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
        public void HandleAttackInputPerformed()
        {
            if (_healthModule.IsReceivingDamage || _movementController.IsMovingBackwards()
                || _movementController.IsJumping || _movementController.IsFalling)
            {
                _attackController.StopAttacking();
                return;
            }

            _attackController.StartAttacking();
        }

        /// <summary>
        /// Stops triggering the attack action
        /// </summary>
        /// <param name="obj"></param>
        public void HandleAttackInputCanceled()
        {
            _attackController.StopAttacking();
        }

        #endregion        

        #endregion


        private void Die()
        {
            DisablePlayerActionMap();
        }
    }
}