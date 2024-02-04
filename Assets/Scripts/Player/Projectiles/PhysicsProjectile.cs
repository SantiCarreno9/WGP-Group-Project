/* PhysicsProjectile.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script uses physics to move and collide with objects
 * 
 */
using UnityEngine;

public class PhysicsProjectile : Projectile
{
    [SerializeField] private Rigidbody _rigidbody;

    /// <summary>
    /// Applies a force to the rigidbody to move forward
    /// </summary>
    public override void Shoot()
    {
        base.Shoot();
        _rigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
    }

    /// <summary>
    /// Triggers the HitTarget method from the parent's class as long as it hit 
    /// an object that implements the IHealth interface
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Transform parent = other.transform.parent;
        if (parent && parent.TryGetComponent(out IHealth target))
        {
            HitTarget(target);
            Deactivate();
        }
    }

    /// <summary>
    /// Stops the rigidbody's movement
    /// </summary>
    protected override void Deactivate()
    {
        _rigidbody.velocity = Vector2.zero;
        base.Deactivate();
    }
}
