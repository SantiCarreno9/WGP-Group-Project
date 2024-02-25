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

        private int _animRotation = Animator.StringToHash("Rotation");
        private int _animForward = Animator.StringToHash("Forward");
        private int _animIsRunning = Animator.StringToHash("IsSprinting");
        private int _animIsJumping = Animator.StringToHash("IsJumping");
        private int _animIsGrounded = Animator.StringToHash("IsGrounded");
        private int _animIsFalling = Animator.StringToHash("IsFalling");
        private int _animIsGettingHit = Animator.StringToHash("GetHit");
        private int _animIsGettingDamage = Animator.StringToHash("IsGettingDamage");
        private int _animIsLanding = Animator.StringToHash("IsLanding");

        private Vector2 _gradualMovement = Vector2.zero;

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
            float speedMultiplier = _movementController.IsSprinting() ? 2 : 1;
            _gradualMovement = _movementController.GetGradualMovement();            
            _gradualMovement.y = Mathf.Abs(_gradualMovement.y);

            _animator.SetFloat(_animRotation, _gradualMovement.x * speedMultiplier);
            _animator.SetFloat(_animForward, _gradualMovement.y * speedMultiplier);
            _animator.SetBool(_animIsRunning, _movementController.IsSprinting());
            _animator.SetBool(_animIsFalling, _movementController.IsFalling);
            _animator.SetBool(_animIsGrounded, _movementController.IsGrounded);
            _animator.SetBool(_animIsJumping, _movementController.IsJumping);
            _animator.SetBool(_animIsJumping, _movementController.IsJumping);

            _movementController.IsLanding = _animator.GetBool(_animIsLanding);
            _healthModule.SetReceivingDamageState(_animator.GetBool(_animIsGettingDamage));
        }

        public void Damage() => _animator.SetTrigger(_animIsGettingHit);

        public void Die() => _animator.Play("Die");
    }
}