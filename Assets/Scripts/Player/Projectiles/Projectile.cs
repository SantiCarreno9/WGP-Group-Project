/* Projectile.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script contains the basic logic for the projectile shot by the player
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected float timeOutDelay = 3f;
    [SerializeField] private int _damagePoints = 10;

    private IObjectPool<Projectile> _objectPool;
    public IObjectPool<Projectile> ObjectPool { set => _objectPool = value; }

    public virtual void Shoot() { }

    /// <summary>
    /// Starts a timer to automatically deactivate the projectile
    /// </summary>
    public void StartDeactivationTimer()
    {
        StartCoroutine(DeactivationCoroutine());
    }

    /// <summary>
    /// Waits for some seconds to deactivate the instance in case it didn't collide with
    /// an enemy
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator DeactivationCoroutine()
    {
        yield return new WaitForSeconds(timeOutDelay);
        Deactivate();
    }

    /// <summary>
    /// Places the instance into the pool
    /// </summary>
    protected virtual void Deactivate()
    {
        _objectPool.Release(this);
    }

    /// <summary>
    /// Damages the target
    /// </summary>
    /// <param name="target"></param>
    protected void HitTarget(IHealth target)
    {
        target.Damage(_damagePoints);
    }
}
