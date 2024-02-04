/* AimController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script manages the auto-aim mechanic 
 * It looks for an enemy within a specific range and fixes the aim at it
 * 
 */
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
    private bool _targetFound = false;
    private Collider[] _foundColliders = new Collider[5];

    private Transform _target = null;
    private bool _aimEnabled = false;

    private void Start()
    {
        _boxCastCenter = transform.position + transform.forward * _boxCastCenterOffset.z;
    }

    public void EnableAim() => _aimEnabled = true;

    public void DisableAim() => _aimEnabled = false;

    /// <summary>
    /// Updates the aim direction as long as the enemy (if there's one) is within
    /// the rotation range
    /// </summary>
    private void UpdateAimDirection()
    {
        DetectEnemies();
        if (_targetFound)
        {
            _autoAim.LookAt(_target);
            Vector3 aimAngle = _autoAim.localEulerAngles;
            //Adjust the angles up to 180 to determine if the target is within the boundaries
            float horizontalAngle = (aimAngle.y > 180) ? Mathf.Abs(360 - aimAngle.y) : aimAngle.y;
            float verticalAngle = (aimAngle.x > 180) ? Mathf.Abs(360 - aimAngle.x) : aimAngle.x;

            //Checks if the target is within the angle boundaries
            if (horizontalAngle <= _maxHorizontalAngle && verticalAngle <= _maxVerticalAngle)
                _aimCenter.localRotation = _autoAim.localRotation;
            else _aimCenter.localRotation = Quaternion.Euler(Vector3.zero);
        }
        //In case there is no target it will place it at the default position
        else _aimCenter.rotation = transform.rotation;
    }

    //private void Update()
    //{
    //    //GIZMOS TEST ONLY
    //    _boxCastCenter = transform.position + transform.forward * _boxCastCenterOffset.z + transform.up * _boxCastCenterOffset.y;
    //}

    /// <summary>
    /// Creates an OverlapBox to detect enemies who are close to the player and selects one as the target
    /// </summary>
    private void DetectEnemies()
    {
        _boxCastCenter = transform.position + transform.forward * _boxCastCenterOffset.z + transform.up * _boxCastCenterOffset.y;
        int amountOfColliders = Physics.OverlapBoxNonAlloc(_boxCastCenter, _boxCastHalfSize, _foundColliders, transform.rotation, _targetLayer.value);
        _targetFound = amountOfColliders > 0;
        //In Case at least one enemy is found it will select the closest one
        if (_targetFound)
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
        else _target = null;

    }

    private void FixedUpdate()
    {
        if (_aimEnabled)
            UpdateAimDirection();
    }

    //DEBUGGING ONLY
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position + _boxCastCenterOffset, _boxCastHalfSize * 2);
    //}
}
