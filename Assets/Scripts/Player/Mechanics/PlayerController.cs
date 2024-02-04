/* PlayerController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script performs all the actions and animations for the main character
 * according to the inputs from the user.
 * It also handles the health system of the player
 * 
 */
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IHealth
{
    private PlayerInputs _inputs;
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
    [SerializeField] private float _damageTimeout = 0.2f;

    private float _speed = 0;
    private const float _gravity = -9.8f;
    private bool _isMoving = false;
    private bool _isGrounded = false;
    private bool _isRunning = false;
    private bool _isJumping = false;
    private bool _isFalling = false;
    private float _yVelocity = 0;
    private bool _isMovingBackwards = false;
    private Vector3 _movement;

    [Header("Animations")]
    [SerializeField] private Animator _animator;

    private int _animRotating = Animator.StringToHash("rotation");
    private int _animForward = Animator.StringToHash("forward");
    private int _animIsMoving = Animator.StringToHash("isMoving");
    private int _animIsJumping = Animator.StringToHash("isJumping");
    private int _animIsGrounded = Animator.StringToHash("isGrounded");
    private int _animIsFalling = Animator.StringToHash("isFalling");
    private int _animIsGettingHit = Animator.StringToHash("GetHit");

    private Vector2 _movementAnimation = Vector2.zero;

    [Header("Health")]
    [SerializeField] private int _maxHealthPoints = 200;
    public int HealthPoints => _healthPoints;
    private int _healthPoints = 0;

    [Header("Attack")]
    [SerializeField] private AttackController _attackController;

    private bool _canAttack = true;
    private bool _isAttacking = false;

    private float _recoveryTime = 0f;
    private bool _interactionsEnabled = true;


    void Awake()
    {
        //Initializes variables
        _characterController = GetComponent<CharacterController>();
        _speed = _walkingSpeed;
        _healthPoints = _maxHealthPoints;

        //Creates a new instance of the PlayerInputs class and subscribes to the events of the user inputs
        _inputs = new PlayerInputs();
        _inputs.Player.Move.performed += Move_performed;
        _inputs.Player.Move.canceled += Move_canceled;

        _inputs.Player.Run.performed += Run_performed;
        _inputs.Player.Run.canceled += Run_canceled;

        _inputs.Player.Jump.performed += context => Jump();

        _inputs.Player.Fire.performed += Fire_performed;
        _inputs.Player.Fire.canceled += Fire_canceled;
    }


    #region USER INPUTS EVENTS

    /// <summary>
    /// Reads the movement input and updates the avatar orientation
    /// </summary>
    /// <param name="obj"></param>
    private void Move_performed(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.IsGamePaused())
            return;

        _movementInputs = obj.ReadValue<Vector2>();
        UpdateAvatarOrientation();
        _isMoving = true;
    }

    /// <summary>
    /// Resets the movement Vector and updates the avatar orientation
    /// </summary>
    /// <param name="obj"></param>
    private void Move_canceled(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.IsGamePaused())
            return;

        _movementInputs = Vector2.zero;
        UpdateAvatarOrientation();
        _isMoving = false;
    }

    /// <summary>
    /// Sets the movement speed to the walking speed
    /// </summary>
    /// <param name="obj"></param>
    private void Run_canceled(InputAction.CallbackContext obj)
    {
        _isRunning = false;
        _speed = _walkingSpeed;
    }

    /// <summary>
    /// Sets the movement speed to the running speed
    /// </summary>
    /// <param name="obj"></param>
    private void Run_performed(InputAction.CallbackContext obj)
    {
        _speed = _runningSpeed;
        _isRunning = true;
    }

    /// <summary>
    /// Ensures that the user is able to attack, if so it starts triggering the attack action
    /// </summary>
    /// <param name="obj"></param>
    private void Fire_performed(InputAction.CallbackContext obj)
    {
        _canAttack = (!IsGettingHit() && !_isMovingBackwards);
        if (_canAttack)
        {
            _attackController.StartAttacking();
            _isAttacking = true;
        }
    }

    /// <summary>
    /// Stops triggering the attack action
    /// </summary>
    /// <param name="obj"></param>
    private void Fire_canceled(InputAction.CallbackContext obj)
    {
        if (_isAttacking)
        {
            _isAttacking = false;
            _attackController.StopAttacking();
        }
    }

    #endregion

    private void OnEnable() => _inputs.Enable();

    private void OnDisable() => _inputs.Disable();

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused())
            return;
        //Allows to control the user as long as it's not receiving damage or 
        //the interactions are enabled
        if (_interactionsEnabled && !IsGettingHit())
        {
            Move();
            UpdateAnimations();
        }
    }

    #region MOVEMENT

    private void Move()
    {
        //Calculates the movement vector
        _movement = transform.forward * _movementInputs.y * _speed;
        _isGrounded = _characterController.isGrounded;

        //Triggers different states according to the user y velocity and grounded status
        if (_isGrounded && _yVelocity < 0.0f)
        {
            _yVelocity = -1.0f;
            _isJumping = false;
            _isFalling = false;
        }
        else
        {
            _isFalling = true;
            _yVelocity += _gravity * _gravityMultiplier * Time.deltaTime;
        }

        _movement.y = _yVelocity;
        //Moves the character controller according to the input received
        _characterController.Move(_movement * Time.deltaTime);
        transform.Rotate(Vector3.up * _rotatingSpeed * _movementInputs.x, Space.Self);
    }

    /// <summary>
    /// Sets the direction the avatar is facing
    /// </summary>
    private void UpdateAvatarOrientation()
    {
        _isMovingBackwards = _movementInputs.y < 0;
        _avatar.transform.localRotation = (_isMovingBackwards) ? Quaternion.Euler(Vector3.up * 180)
            : Quaternion.Euler(Vector3.zero);
    }

    /// <summary>
    /// Increases the y velocity to simulate jumping
    /// </summary>
    private void Jump()
    {
        if (_isGrounded && !_isJumping)
        {
            _yVelocity += _jumpHeight;
            _isJumping = true;
        }
    }

    private bool IsGettingHit() => Time.time < _recoveryTime;

    #endregion

    /// <summary>
    /// Disables the character control
    /// </summary>
    private void DeactivateController()
    {
        _inputs.Disable();
        _characterController.enabled = false;
        _interactionsEnabled = false;
    }

    #region ANIMATIONS

    /// <summary>
    /// Updates the values of all the animation parameters
    /// </summary>
    private void UpdateAnimations()
    {
        float multiplier = (!_isRunning ? 0.5f : _speed / _walkingSpeed);
        _movementAnimation.x = _movementInputs.x * 0.5f;
        _movementAnimation.y = Mathf.Abs(_movementInputs.y * multiplier);

        _animator.SetBool(_animIsMoving, _isMoving);
        _animator.SetFloat(_animRotating, _movementAnimation.x);
        _animator.SetFloat(_animForward, _movementAnimation.y);
        _animator.SetBool(_animIsFalling, _isFalling);
        _animator.SetBool(_animIsGrounded, _isGrounded);
        _animator.SetBool(_animIsJumping, _isJumping);
    }

    /// <summary>
    /// Triggers the "getting hit" animation and updates the recovery time
    /// that has to pass to allow the user to control the character
    /// </summary>
    public void Damage()
    {
        _recoveryTime = Time.time + _damageTimeout;
        _animator.SetTrigger(_animIsGettingHit);
    }

    /// <summary>
    /// Plays the dead animation and disables the controls
    /// </summary>
    public void Die()
    {
        _animator.Play("Die");
        DeactivateController();
        GameManager.Instance.ShowGameOverScreen();
    }
    #endregion

    #region HEALTH

    /// <summary>
    /// Reduces the character's health and trigger the correct animations
    /// </summary>
    /// <param name="points"></param>
    public void Damage(int points)
    {
        _healthPoints -= points;
        if (_healthPoints <= 0)
            Die();
        else Damage();
    }

    #endregion
}
