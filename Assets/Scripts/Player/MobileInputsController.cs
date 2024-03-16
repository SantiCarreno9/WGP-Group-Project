/* MobileInputsController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 03/14/2024
 * 
 * This script manages the UI input for Android platform
 * 
 */
using UnityEngine;
using UnityEngine.UI;

namespace Character.Mobile
{
    public class MobileInputsController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        [Header("UI")]
        [SerializeField] private Joystick _joystick;        
        [SerializeField] private CustomButton _joystickButton;
        [SerializeField] private Toggle _sprintToggle;
        [SerializeField] private CustomButton _jumpButton;
        [SerializeField] private CustomButton _attackButton;

        private bool _isJoystickPressed = false;

        private void Awake()
        {
#if !UNITY_ANDROID
         gameObject.SetActive(false);   
#endif
        }

        private void OnEnable()
        {
            _joystickButton.OnClicked += HandleMovementPerformed;
            _joystickButton.OnReleased += HandleMovementCanceled;

            _sprintToggle.onValueChanged.AddListener(HandleSprint);            

            _jumpButton.OnClicked += HandleJumpPerformed;

            _attackButton.OnClicked += HandleAttackPerformed;
            _attackButton.OnReleased += HandleAttackCanceled;
        }

        private void OnDisable()
        {
            _joystickButton.OnClicked += HandleMovementPerformed;
            _joystickButton.OnReleased += HandleMovementCanceled;

            _sprintToggle.onValueChanged.RemoveListener(HandleSprint);

            _jumpButton.OnClicked -= HandleJumpPerformed;

            _attackButton.OnClicked -= HandleAttackPerformed;
            _attackButton.OnReleased -= HandleAttackCanceled;
        }

        void Update()
        {
            if (_isJoystickPressed)
                HandleMovementPerformed();
        }

        private void HandleMovementPerformed()
        {
            _isJoystickPressed = true;
            _playerController.HandleMovementInputPerformed(_joystick.Direction);
        }

        private void HandleMovementCanceled()
        {
            _isJoystickPressed = false;
            _playerController.HandleMovementInputCanceled();
        }

        private void HandleSprint(bool performerd)
        {
            if (performerd) _playerController.HandleSprintInputPerformed();
            else _playerController.HandleSprintInputCanceled();            
        }        

        private void HandleJumpPerformed() => _playerController.HandleJumpInputPerformed();

        private void HandleAttackPerformed() => _playerController.HandleAttackInputPerformed();

        private void HandleAttackCanceled() => _playerController.HandleAttackInputCanceled();
    }
}