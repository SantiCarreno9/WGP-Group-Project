/* MovementController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script manages the movement mechanics of the player
 * 
 */
using UnityEngine;
namespace Character
{
    public class MovementController : MonoBehaviour
    {
        private Vector2 _movementInputs;

        [Header("Character Controller")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private GameObject _avatar;

        [Header("Movements")]
        [SerializeField] private float _walkingSpeed = 1f;
        [SerializeField] private float _rotatingSpeed = 0.3f;
        [SerializeField] private float _gravityMultiplier = 2f;
        [SerializeField] private float _jumpHeight = 2.0f;
        private float _walkSpeedMultiplier = 1;
        private float _rotateSpeedMultiplier = 1;
        private const float _gravity = -9.8f;
        private Vector2 _gradualMovement = Vector2.zero;

        [Header("Sprint")]
        [SerializeField] private float _sprintWalkSpeedMultiplier = 2f;
        [SerializeField] private float _sprintRotateSpeedMultiplier = 2f;
        [SerializeField] private float _sprintDuration = 2.0f;
        [SerializeField] private float _sprintRecoveryRate = 0.5f;

        private float _sprintRemainingTime = 0;
        private bool _isSprinting = false;

        public bool IsGrounded { get; private set; } = false;
        public bool IsJumping { get; private set; } = false;
        public bool IsFalling { get; private set; } = false;
        private float _yVelocity = 0;
        private Vector3 _movement;


        void Awake()
        {
            //Initializes variables                        
            _sprintRemainingTime = _sprintDuration;
        }

        public Vector2 GetMovementDirection() => _movementInputs;
        public Vector2 GetGradualMovement() => _gradualMovement;

        private void Update()
        {
            Move();
            UpdateSprintUsage();
        }

        /// <summary>
        /// Reads the movement input and updates the avatar orientation
        /// </summary>
        /// <param name="movementInputs"></param>
        public void SetUserMovementInput(Vector2 movementInputs)
        {
            _movementInputs = movementInputs;
            UpdateAvatarOrientation();
        }

        private void Move()
        {
            _walkSpeedMultiplier = (_isSprinting && CanSprint()) ? _sprintWalkSpeedMultiplier : 1;
            _rotateSpeedMultiplier = (_isSprinting && CanSprint()) ? _rotateSpeedMultiplier : 1;
                        
            _gradualMovement.x = Mathf.SmoothStep(_gradualMovement.x, _movementInputs.x, 0.1f);
            _gradualMovement.y = Mathf.SmoothStep(_gradualMovement.y, _movementInputs.y * _walkSpeedMultiplier, 0.2f);
            //_gradualMovement.y = Mathf.SmoothStep(_gradualMovement.y, _movementInputs.y, 0.2f);

            //Calculates the movement vector            
            //_movement = transform.forward * _gradualMovement.y * _walkingSpeed * _walkSpeedMultiplier;
            _movement = transform.forward * _gradualMovement.y * _walkingSpeed;
            IsGrounded = _characterController.isGrounded;

            //Triggers different states according to the user y velocity and grounded status
            if (IsGrounded && _yVelocity < 0.0f)
            {
                _yVelocity = -1.0f;
                IsJumping = false;
                IsFalling = false;
            }
            else
            {
                IsFalling = true;
                _yVelocity += _gravity * _gravityMultiplier * Time.deltaTime;
            }

            _movement.y = _yVelocity;

            //Moves the character controller according to the input received
            _characterController.Move(_movement * Time.deltaTime);
            _characterController.transform.Rotate(Vector3.up * _rotatingSpeed * _walkSpeedMultiplier * _gradualMovement.x, Space.Self);
        }

        public bool IsMovingBackwards() => _movementInputs.y < 0;

        /// <summary>
        /// Sets the direction the avatar is facing
        /// </summary>
        private void UpdateAvatarOrientation()
        {
            _avatar.transform.localRotation = (IsMovingBackwards()) ? Quaternion.Euler(Vector3.up * 180)
                : Quaternion.Euler(Vector3.zero);
        }

        /// <summary>
        /// Increases the y velocity to simulate jumping
        /// </summary>
        public void Jump()
        {
            if (IsGrounded && !IsJumping)
            {
                _yVelocity += _jumpHeight;
                IsJumping = true;
            }
        }

        #region SPRINT

        public bool IsSprinting() => _walkSpeedMultiplier == _sprintWalkSpeedMultiplier;

        public void StartSprinting() => _isSprinting = true;

        public void StopSprinting() => _isSprinting = false;

        private void UpdateSprintUsage()
        {
            if (_isSprinting && _movementInputs.magnitude != 0)
            {
                if (_sprintRemainingTime > 0)
                    _sprintRemainingTime -= Time.fixedDeltaTime;
                else _sprintRemainingTime = 0;
            }
            else if (!_isSprinting)
            {
                if (_sprintRemainingTime < _sprintDuration)
                    _sprintRemainingTime += Time.fixedDeltaTime * _sprintRecoveryRate;
                else _sprintRemainingTime = _sprintDuration;
            }
        }

        public float GetRemainingSprintPercentage()
        {
            return _sprintRemainingTime / _sprintDuration;
        }

        private bool CanSprint() => _sprintRemainingTime > 0;

        #endregion
    }
}