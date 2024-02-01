using UnityEngine;
using UnityEngine.Pool;

public class WeaponsController : MonoBehaviour
{
    [Tooltip("Prefab to shoot")]
    [SerializeField] private Projectile _projectilePrefab;

    [Tooltip("Spawn position for projectiles")]
    [SerializeField] private Transform _spawnPosition;

    [Tooltip("Time between shots")]
    [SerializeField] private float _cooldown = 0.1f;

    private IObjectPool<Projectile> _projectilePool;

    [SerializeField] private bool _collectionCheck = true;

    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 20;

    [SerializeField] private AimController _aimController;

    private float _nextTimeToShoot = 0;


    private void Awake()
    {
        _projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, _collectionCheck, _defaultCapacity, _maxSize);
    }

    private void OnDestroyPooledObject(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    private void OnReleaseToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnGetFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private Projectile CreateProjectile()
    {
        Projectile instance = Instantiate(_projectilePrefab);
        instance.ObjectPool = _projectilePool;
        return instance;
    }

    public void Shoot()
    {
        if (Time.time < _nextTimeToShoot)
            return;

        _aimController.UpdateAimDirection();
        Projectile projectile = _projectilePool.Get();

        if (projectile == null)
            return;

        projectile.transform.SetPositionAndRotation(_spawnPosition.position, _spawnPosition.rotation);
        projectile.Shoot();
        projectile.StartDeactivationTimer();
        _nextTimeToShoot = Time.time + _cooldown;
    }

}
