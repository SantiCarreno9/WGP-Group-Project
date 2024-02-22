using UnityEngine;

namespace Character
{
    public class PlayerAnimationsEvents : MonoBehaviour
    {
        [SerializeField] private PlayerSoundsController _soundsController;
        [SerializeField] private Animator _animator;
        private int _animBlendTree = Animator.StringToHash("IsRunningBlendTree");

        private void Step(AnimationEvent animationEvent)
        {            
            if (animationEvent.animatorClipInfo.weight > 0.5f || !_animator.GetBool(_animBlendTree))
                _soundsController.PlayStepSound();            
        }

        private void Jump() => _soundsController.PlayJumpSound();

        private void Attack() => _soundsController.PlayAttackSound();

        private void Damage() => _soundsController.PlayAttackSound();

        private void Dead() => _soundsController.PlayDeadSound();
    }
}