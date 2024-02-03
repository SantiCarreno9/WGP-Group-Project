
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public int HealthPoints => _healthPoints;
    private int _healthPoints = 0;
    [SerializeField] private int _maxHealthPoints = 200;

    [Space]
    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
        _healthPoints = _maxHealthPoints;
    }

    public void Damage(int points)
    {
        _healthPoints -= points;
        if (_healthPoints <= 0)
            _playerController.Die();
        else _playerController.Damage();
    }    

}
