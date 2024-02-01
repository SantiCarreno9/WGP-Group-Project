using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] private Transform _aimCenter;    

    [SerializeField] private Vector3 _boxCastCenterOffset = Vector3.zero;
    [SerializeField] private Vector3 _boxCastHalfSize = Vector3.one;
    [SerializeField] private LayerMask _targetLayer;

    private Vector3 _boxCastCenter;
    private bool _hitDetect = false;
    private Collider[] _foundColliders = new Collider[5];

    private Transform _target = null;

    private void Start()
    {
        _boxCastCenter = transform.position + transform.forward * _boxCastCenterOffset.z;
    }

    public void UpdateAimDirection()
    {
        DetectEnemies();
        if (_hitDetect)
        {
            Vector3 direction = _target.position - _aimCenter.position;
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90;
            angle = Mathf.Abs(angle);
            Debug.Log(angle);
            if (angle <= 50 && angle >= 0)
                _aimCenter.LookAt(_target);
            else _aimCenter.rotation = transform.rotation;
        }
        else _aimCenter.rotation = transform.rotation;
    }

    //private void Update()
    //{
    //    //GIZMOS TEST ONLY
    //    _boxCastCenter = transform.position + transform.forward * _boxCastCenterOffset.z + transform.up * _boxCastCenterOffset.y;
    //}

    private void DetectEnemies()
    {
        _boxCastCenter = transform.position + transform.forward * _boxCastCenterOffset.z + transform.up * _boxCastCenterOffset.y;
        int amountOfColliders = Physics.OverlapBoxNonAlloc(_boxCastCenter, _boxCastHalfSize, _foundColliders, transform.rotation, _targetLayer.value);
        _hitDetect = amountOfColliders > 0;
        if (_hitDetect)
        {
            float minDistance = float.MaxValue;
            int colliderIndex = -1;
            for (int i = 0; i < _foundColliders.Length; i++)
            {
                if (_foundColliders[i] == null)
                    break;

                float distance = Vector3.Distance(transform.position, _foundColliders[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    colliderIndex = i;
                }
            }
            _target = _foundColliders[colliderIndex].transform;
            System.Array.Clear(_foundColliders, 0, 5);
        }
        else
            _target = null;
    }

    //private void FixedUpdate()
    //{
    //    UpdateAimDirection();
    //}

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position + _boxCastCenterOffset, _boxCastHalfSize * 2);
    //}
}
