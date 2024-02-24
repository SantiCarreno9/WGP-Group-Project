using UnityEngine;

namespace Character.UI
{
    public class PlayerStatsPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerStatsView _view;
        [SerializeField] private HealthModule _healthModule;
        [SerializeField] private MovementController _movementController;        

        private void OnEnable()
        {
            _healthModule.OnHealthChanged += UpdateHealth;
        }

        private void OnDisable()
        {
            _healthModule.OnHealthChanged -= UpdateHealth;
        }

        private void Start()
        {
            UpdateHealth(_healthModule.HealthPoints);
        }

        private void UpdateHealth(int points)
        {
            float percentage = (float)points / (float)_healthModule.GetMaxHealth();
            _view.UpdateHealth(percentage);
        }

        private void Update()
        {
            _view.UpdateSprint(_movementController.GetRemainingSprintPercentage());
        }

    }
}