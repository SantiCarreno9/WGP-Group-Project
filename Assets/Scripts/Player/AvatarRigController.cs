using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
//using 

public class AvatarRigController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [Header("Aim")]
    [SerializeField] private Rig _aimRig;
    [SerializeField] private Transform _handRigTarget;
    [SerializeField] private Transform _shootingPoint;
    
    private bool _isAimRigEnabled = false;

    private void OnEnable()
    {
        _playerController.OnShootStarted += EnableAimRig;
        _playerController.OnShootCanceled += DisableAimRig;
    }

    private void OnDisable()
    {
        _playerController.OnShootStarted -= EnableAimRig;
        _playerController.OnShootCanceled -= DisableAimRig;
    }

    private void Awake()
    {
        DisableAimRig();
    }

    public void EnableAimRig()
    {
        DOTween.To(() => _aimRig.weight, x => _aimRig.weight = x, 1, 0.1f).onComplete+=()=>
        {
            _isAimRigEnabled = true;
        };
        //_isAimRigEnabled = true;
        //_aimRig.weight = 1.0f;

    }

    public void DisableAimRig()
    {
        DOTween.To(() => _aimRig.weight, x => _aimRig.weight = x, 0, 0.1f).onComplete += () =>
        {
            _isAimRigEnabled = false;
        };
        //_aimRig.weight = 0;
        //_isAimRigEnabled = false;
    }            

    // Update is called once per frame
    void Update()
    {
        if (_isAimRigEnabled)
        {
            _handRigTarget.transform.position = _shootingPoint.position;
        }
    }
}
