/* AnimationController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script performs all the animations according to parameters from
 * other modules, such as MovementController and HealthModule
 * 
 */
using UnityEngine;

namespace Character
{
    public class AnimationController : MonoBehaviour
    {
        [Header("Modules")]
        [SerializeField] private MovementController _movementController;
        [SerializeField] private HealthModule _healthModule;

        [Space]
        [Header("Animations")]
        [SerializeField] private Animator _animator;

        private int _animRotating = Animator.StringToHash("rotation");
        private int _animForward = Animator.StringToHash("forward");
        private int _animIsMoving = Animator.StringToHash("isMoving");
        private int _animIsJumping = Animator.StringToHash("isJumping");
        private int _animIsGrounded = Animator.StringToHash("isGrounded");
        private int _animIsFalling = Animator.StringToHash("isFalling");
        private int _animIsGettingHit = Animator.StringToHash("GetHit");

        private Vector2 _movement = Vector2.zero;

        private void OnEnable()
        {
            _healthModule.OnDamageReceived += Damage;
            _healthModule.OnDie += Die;
        }

        private void OnDisable()
        {
            _healthModule.OnDamageReceived -= Damage;
            _healthModule.OnDie -= Die;
        }

        private void Update()
        {
            UpdateAnimations();
        }

        /// <summary>
        /// Updates the values of all the animation parameters
        /// </summary>
        private void UpdateAnimations()
        {
            //float multiplier = (!_movementController.IsRunning ? 0.5f : _speed / _walkingSpeed);
            float multiplier = (!_movementController.IsSprinting() ? 0.5f : 1);
            _movement.x = _movementController.GetMovementDirection().x * 0.5f;
            _movement.y = Mathf.Abs(_movementController.GetMovementDirection().y * multiplier);

            _animator.SetBool(_animIsMoving, _movementController.IsMoving);
            _animator.SetFloat(_animRotating, _movement.x);
            _animator.SetFloat(_animForward, _movement.y);
            _animator.SetBool(_animIsFalling, _movementController.IsFalling);
            _animator.SetBool(_animIsGrounded, _movementController.IsGrounded);
            _animator.SetBool(_animIsJumping, _movementController.IsJumping);
        }
        
        public void Damage() => _animator.SetTrigger(_animIsGettingHit);
        
        public void Die() => _animator.Play("Die");
    }
}