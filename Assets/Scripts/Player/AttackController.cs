using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private AimController _aimController;
    [SerializeField] private AvatarRigController _avatarRigController;
    [SerializeField] private WeaponsController _weaponController;

    private bool _isAttacking = false;

    public void StartAttacking()
    {
        _aimController.EnableAim();
        _avatarRigController.EnableAimRig();
        _avatarRigController.OnAimRigActivated += TriggerAttack;
    }

    private void TriggerAttack()
    {
        _isAttacking = true;
    }

    public void StopAttacking()
    {
        _aimController.DisableAim();
        _avatarRigController.DisableAimRig();
        _avatarRigController.OnAimRigActivated -= TriggerAttack;
        _isAttacking = false;
    }

    private void Update()
    {
        if (_isAttacking)
            _weaponController.Shoot();
    }
}
