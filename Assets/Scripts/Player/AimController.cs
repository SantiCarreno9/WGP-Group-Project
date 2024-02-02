using UnityEngine;

public class AimController : MonoBehaviour
{
    [Header("Aim Components")]
    [SerializeField] private Transform _autoAim;
    [SerializeField] private Transform _aimCenter;

    [Header("Aim Constraints")]
    [SerializeField] private float _maxHorizontalAngle = 50;
    [SerializeField] private float _maxVerticalAngle = 30;

    [Header("Overlap Box")]
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
            _autoAim.LookAt(_target);
            Vector3 aimAngle = _autoAim.localEulerAngles;
            float horizontalAngle = (aimAngle.y > 180) ? Mathf.Abs(360 - aimAngle.y) : aimAngle.y;
            float verticalAngle = (aimAngle.x > 180) ? Mathf.Abs(360 - aimAngle.x) : aimAngle.x;

            Debug.Log("Horizontal: " + horizontalAngle + " Vertical: " + verticalAngle);

            if (horizontalAngle <= _maxHorizontalAngle && verticalAngle <= _maxVerticalAngle)
                _aimCenter.localRotation = _autoAim.localRotation;
            else _aimCenter.localRotation = Quaternion.Euler(Vector3.zero);
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

    private void FixedUpdate()
    {
        UpdateAimDirection();
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position + _boxCastCenterOffset, _boxCastHalfSize * 2);
    //}
}
