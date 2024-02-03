using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputs _inputs;
    private Vector2 _movementInputs;

    [Header("Character Controller")]
    private CharacterController _characterController;
    [SerializeField] private GameObject _avatar;

    [Header("Movements")]
    [SerializeField] private float _walkingSpeed = 5f;
    [SerializeField] private float _runningSpeed = 7f;
    [SerializeField] private float _rotatingSpeed = 1f;
    [SerializeField] private float _gravityMultiplier = 2f;
    [SerializeField] private float _jumpHeight = 2.0f;
    [SerializeField] private float _damageTimeout = 0.2f;

    private float _speed = 0;
    private const float gravity = -9.8f;
    private bool _isGrounded;
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

    //[Header("Ground Detection")]
    //[SerializeField] private Transform _groundCheck;
    //[SerializeField] private float _groundRadius = 0.5f;
    //[SerializeField] private LayerMask _groundMask;


    [Header("Attack Components")]
    [SerializeField] private AttackController _attackController;
    public UnityAction OnShootStarted;
    public UnityAction OnShootCanceled;

    private bool _canAttack = true;
    private bool _isAttacking = false;
    
    private float _recoveryTime = 0f;
    private bool _interactionsEnabled = true;


    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputs = new PlayerInputs();
        _inputs.Player.Move.performed += Move_performed;
        _inputs.Player.Move.canceled += Move_canceled;

        _inputs.Player.Run.performed += Run_performed;
        _inputs.Player.Run.canceled += Run_canceled;

        _inputs.Player.Jump.performed += context => Jump();

        _inputs.Player.Fire.performed += Fire_performed;
        _inputs.Player.Fire.canceled += Fire_canceled;

        _speed = _walkingSpeed;
    }


    #region USER INPUTS EVENTS

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        _canAttack = (!IsGettingHit() && !_isMovingBackwards);
        if (_canAttack)
        {
            _attackController.StartAttacking();
            _isAttacking = true;
            OnShootStarted?.Invoke();
        }
    }

    private void Fire_canceled(InputAction.CallbackContext obj)
    {
        if (_isAttacking)
        {
            _isAttacking = false;
            _attackController.StopAttacking();
            OnShootCanceled?.Invoke();
        }
    }

    private void Run_canceled(InputAction.CallbackContext obj)
    {
        _isRunning = false;
        _speed = _walkingSpeed;
    }

    private void Run_performed(InputAction.CallbackContext obj)
    {
        _speed = _runningSpeed;
        _isRunning = true;
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        _movementInputs = obj.ReadValue<Vector2>();
        UpdateAvatarOrientation();
        _animator.SetBool(_animIsMoving, true);

    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        _movementInputs = Vector2.zero;
        UpdateAvatarOrientation();
        _animator.SetBool(_animIsMoving, false);
    }


    #endregion

    private void OnEnable() => _inputs.Enable();

    private void OnDisable() => _inputs.Disable();

    private void Update()
    {
        if (_interactionsEnabled && !IsGettingHit())
        {
            Move();
            UpdateAnimations();
        }
        //TEST ONLY
        if (Input.GetKeyDown(KeyCode.M))
            Die();
        if (Input.GetKeyDown(KeyCode.N))
            Damage();
    }

    #region MOVEMENT

    private void Move()
    {
        //Translation
        _movement = transform.forward * _movementInputs.y * _speed;
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _yVelocity < 0.0f)
        {
            _yVelocity = -1.0f;
            _isJumping = false;
            _isFalling = false;
        }
        else
        {
            _isFalling = true;
            _yVelocity += gravity * _gravityMultiplier * Time.deltaTime;
        }

        _movement.y = _yVelocity;
        _characterController.Move(_movement * Time.deltaTime);
        transform.Rotate(Vector3.up * _rotatingSpeed * _movementInputs.x, Space.Self);
    }

    private void UpdateAvatarOrientation()
    {
        _isMovingBackwards = _movementInputs.y < 0;
        _avatar.transform.localRotation = (_isMovingBackwards) ? Quaternion.Euler(Vector3.up * 180)
            : Quaternion.Euler(Vector3.zero);
    }

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

    private void DeactivateController()
    {
        _inputs.Disable();
        _characterController.enabled = false;
    }

    #region ANIMATIONS

    private void UpdateAnimations()
    {
        float multiplier = (!_isRunning ? 0.5f : _speed / _walkingSpeed);
        _movementAnimation.x = _movementInputs.x * 0.5f;
        _movementAnimation.y = Mathf.Abs(_movementInputs.y * multiplier);

        _animator.SetFloat(_animRotating, _movementAnimation.x);
        _animator.SetFloat(_animForward, _movementAnimation.y);
        _animator.SetBool(_animIsFalling, _isFalling);
        _animator.SetBool(_animIsGrounded, _isGrounded);
        _animator.SetBool(_animIsJumping, _isJumping);        
    }
    

    public void Damage()
    {
        _recoveryTime = Time.time + _damageTimeout;
        _animator.SetTrigger(_animIsGettingHit);
    }

    public void Die()
    {
        _animator.Play("Die");
        DeactivateController();
    }
    #endregion
}
