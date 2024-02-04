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

        if(other.transform.root.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Damage(5);
        }
    }

    protected override void Deactivate()
    {
        _rigidbody.velocity = Vector2.zero;
        base.Deactivate();
    }
}
