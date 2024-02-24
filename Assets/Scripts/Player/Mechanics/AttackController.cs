/* AttackController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script works as a bridge between the PlayerController and WeaponController 
 * in order to perform the attack action.
 * It also communicates with the AimController and AvatarRigController to update the
 * character's hand position to aim at the enemies
 * 
 */
using UnityEngine;

namespace Character
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private AimController _aimController;
        [SerializeField] private AvatarRigController _avatarRigController;
        [SerializeField] private WeaponController _weaponController;

        private bool _isAttacking = false;

        /// <summary>
        /// Enables the auto-aim mechanic along with the character's hand animation
        /// </summary>
        public void StartAttacking()
        {
            _aimController.EnableAim();            
            _avatarRigController.EnableAimRig();
            _avatarRigController.OnAimRigActivated += TriggerAttack;
        }

        /// <summary>
        /// Updates the attacking property to perform a continuous attack 
        /// </summary>
        private void TriggerAttack() => _isAttacking = true;

        /// <summary>
        /// Disables the auto-aim mechanic, returns the hand to its original position
        /// and updates the attacking property to stop attacking
        /// </summary>
        public void StopAttacking()
        {
            _aimController.DisableAim();
            _avatarRigController.DisableAimRig();
            _avatarRigController.OnAimRigActivated -= TriggerAttack;
            _isAttacking = false;
        }

        private void Update()
        {
            //Continuously triggers the attack action as long as the user keeps performing it
            if (_isAttacking)
                _weaponController.Shoot();
        }
    }
}