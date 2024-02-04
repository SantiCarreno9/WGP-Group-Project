/* AvatarRigController.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script manages the hand's Rig component 
 * in order to update the hand's position towards the target
 * while it's attacking
 * 
 */

using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class AvatarRigController : MonoBehaviour
{
    [Header("Aim")]
    [SerializeField] private Rig _aimRig;
    [SerializeField] private Transform _handRigTarget;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private float _transitionDuration = 0.5f;

    public UnityAction OnAimRigActivated;
    public UnityAction OnAimRigDeactivated;

    private bool _isAimRigEnabled = false;

    private void Awake()
    {
        DisableAimRig();
    }

    /// <summary>
    /// Increases the rig's weight to place the hand at the aim position 
    /// </summary>
    public void EnableAimRig()
    {
        _isAimRigEnabled = true;
        DOTween.To(() => _aimRig.weight, x => _aimRig.weight = x, 1, _transitionDuration).onComplete += () =>
        {
            OnAimRigActivated?.Invoke();
        };
    }

    /// <summary>
    /// Increases the rig's weight to place the hand at the default position 
    /// </summary>
    public void DisableAimRig()
    {
        _isAimRigEnabled = false;
        DOTween.To(() => _aimRig.weight, x => _aimRig.weight = x, 0, _transitionDuration).onComplete += () =>
        {
            OnAimRigDeactivated?.Invoke();
        };
    }

    /// <summary>
    /// Updates the hand's rig's target
    /// </summary>
    private void UpdateAimTargetPosition() => _handRigTarget.transform.position = _shootingPoint.position;


    void Update()
    {
        //Updates the hand's rig's target continuously
        if (_isAimRigEnabled)
            UpdateAimTargetPosition();
    }
}
