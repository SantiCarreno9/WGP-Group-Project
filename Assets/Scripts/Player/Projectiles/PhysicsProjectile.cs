using UnityEngine;

public class PhysicsProjectile : Projectile
{
    [SerializeField] private Rigidbody _rigidbody;

    public override void Shoot()
    {
        base.Shoot();
        _rigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealth target))
            HitTarget(target);
    }

    protected override void Deactivate()
    {
        _rigidbody.velocity = Vector2.zero;
        base.Deactivate();
    }
}
