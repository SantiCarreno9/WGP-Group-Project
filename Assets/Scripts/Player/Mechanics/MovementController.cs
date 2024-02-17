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
        [SerializeField] private float _walkingSpeed = 5f;
        [SerializeField] private float _runningSpeed = 7f;
        [SerializeField] private float _rotatingSpeed = 1f;
        [SerializeField] private float _gravityMultiplier = 2f;
        [SerializeField] private float _jumpHeight = 2.0f;
        private float _speed = 0;
        private const float _gravity = -9.8f;

        [Header("Sprint")]
        [SerializeField] private float _sprintSpeed = 12f;
        [SerializeField] private float _sprintDuration = 2.0f;
        [SerializeField] private float _sprintRecoveryRate = 0.5f;

        private float _sprintRemainingTime = 0;
        private bool _isSprinting = false;

        public bool IsMoving { get; private set; } = false;
        public bool IsGrounded { get; private set; } = false;
        public bool IsJumping { get; private set; } = false;
        public bool IsFalling { get; private set; } = false;
        private float _yVelocity = 0;
        private Vector3 _movement;

        private bool _interactionsEnabled = true;


        void Awake()
        {
            //Initializes variables            
            _speed = _walkingSpeed;
            _sprintRemainingTime = _sprintDuration;
        }

        public Vector2 GetMovementDirection() => _movementInputs;


        private void Update()
        {
            //Allows to control the user as long as it's not receiving damage or 
            //the interactions are enabled
            //if (_interactionsEnabled /*&&*/ /*!IsGettingHit()*/)
            {
                Move();
                UpdateSprintUsage();
            }
        }

        /// <summary>
        /// Reads the movement input and updates the avatar orientation
        /// </summary>
        /// <param name="movementInputs"></param>
        public void SetUserMovementInput(Vector2 movementInputs)
        {
            _movementInputs = movementInputs;
            UpdateAvatarOrientation();
            IsMoving = (movementInputs != Vector2.zero);
        }

        private void Move()
        {
            if (_isSprinting && CanSprint())
                _speed = _sprintSpeed;
            else _speed = _walkingSpeed;

            //Calculates the movement vector
            _movement = transform.forward * _movementInputs.y * _speed;
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
            _characterController.transform.Rotate(Vector3.up * _rotatingSpeed * _movementInputs.x, Space.Self);
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

        public bool IsSprinting() => _isSprinting && _movementInputs.magnitude != 0;

        public void StartSprinting() => _isSprinting = true;

        public void StopSprinting() => _isSprinting = false;

        private void UpdateSprintUsage()
        {
            if (IsSprinting())
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