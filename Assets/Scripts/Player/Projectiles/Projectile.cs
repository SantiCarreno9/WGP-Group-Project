using System;
using System.Collections;
using System.Collections.Generic;
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

    public void StartDeactivationTimer()
    {
        StartCoroutine(DeactivationCoroutine(timeOutDelay));
    }

    private IEnumerator DeactivationCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Deactivate();
    }

    protected virtual void Deactivate()
    {
        _objectPool.Release(this);
    }

    protected void HitTarget(IHealth target)
    {
        target.Damage(_damagePoints);
    }
}
