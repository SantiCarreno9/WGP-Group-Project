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

    public void EnableAimRig()
    {
        _isAimRigEnabled = true;
        DOTween.To(() => _aimRig.weight, x => _aimRig.weight = x, 1, _transitionDuration).onComplete += () =>
        {
            OnAimRigActivated?.Invoke();
        };
    }

    public void DisableAimRig()
    {
        _isAimRigEnabled = false;
        DOTween.To(() => _aimRig.weight, x => _aimRig.weight = x, 0, _transitionDuration).onComplete += () =>
        {
            OnAimRigDeactivated?.Invoke();
        };
    }

    private void UpdateAimTargetPosition() => _handRigTarget.transform.position = _shootingPoint.position;


    void Update()
    {
        if (_isAimRigEnabled)
            UpdateAimTargetPosition();
    }
}
