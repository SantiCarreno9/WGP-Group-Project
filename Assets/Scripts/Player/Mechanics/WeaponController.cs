/* WeaponController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script manages the projectile instantiation and shooting
 * 
 */
using UnityEngine;
using UnityEngine.Pool;

public class WeaponController : MonoBehaviour
{
    [Tooltip("Prefab to shoot")]
    [SerializeField] private Projectile _projectilePrefab;

    [Tooltip("Spawn position for projectiles")]
    [SerializeField] private Transform _spawnPosition;

    [Tooltip("Time between shots")]
    [SerializeField] private float _cooldown = 0.3f;

    private IObjectPool<Projectile> _projectilePool;

    private bool _collectionCheck = true;

    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 20;    

    private float _nextTimeToShoot = 0;

    private void Awake()
    {
        //A projectile pool is created in order to reuse projectiles instances
        _projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, _collectionCheck, _defaultCapacity, _maxSize);
    }

    /// <summary>
    /// Destroys a projectile instance from the pool in case it is above the maximum amount
    /// </summary>
    /// <param name="projectile"></param>
    private void OnDestroyPooledObject(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    /// <summary>
    /// Deactivates the projectile gameobject once it's put into the pool
    /// </summary>
    /// <param name="projectile"></param>
    private void OnReleaseToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    /// <summary>
    /// Activates the projectile gameobject once it's released from the pool
    /// </summary>
    /// <param name="projectile"></param>
    private void OnGetFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    /// <summary>
    /// Instantiates the projectile and updates its references
    /// </summary>
    /// <returns></returns>
    private Projectile CreateProjectile()
    {
        Projectile instance = Instantiate(_projectilePrefab);
        instance.ObjectPool = _projectilePool;
        return instance;
    }

    /// <summary>
    /// Retrieves a projectile from the pool, positions it at the spawn position and shoots it
    /// as long as the fire rate allows it
    /// </summary>
    public void Shoot()
    {
        if (Time.time < _nextTimeToShoot)
            return;
        
        Projectile projectile = _projectilePool.Get();

        if (projectile == null)
            return;

        projectile.transform.SetPositionAndRotation(_spawnPosition.position, _spawnPosition.rotation);
        projectile.Shoot();
        projectile.StartDeactivationTimer();
        _nextTimeToShoot = Time.time + _cooldown;
    }
}
