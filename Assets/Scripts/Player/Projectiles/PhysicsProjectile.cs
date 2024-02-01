using UnityEngine;

public class PhysicsProjectile : Projectile
{
    [SerializeField] private Rigidbody _rigidbody;

    public override void Shoot()
    {
        base.Shoot();
        _rigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
    }

    private void FixedUpdate()
    {
        //if (_shoot)
        //{
        //    _rigidbody.AddForce(_direction * speed, ForceMode.Acceleration);
        //    _shoot = false;
        //}
    }

    protected override void Deactivate()
    {
        _rigidbody.velocity = Vector2.zero;
        base.Deactivate();
    }
}
