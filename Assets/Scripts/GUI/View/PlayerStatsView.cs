using UnityEngine;
using UnityEngine.UI;

namespace Character.UI
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private GameObject _statsContainer;
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _sprintBar;
        private float _healthBarMultiplier = 1.0f;
        private float _sprintBarMultiplier = 1.0f;

        private void Start()
        {
            _healthBarMultiplier = _healthBar.fillAmount;
            _sprintBarMultiplier = _sprintBar.fillAmount;
        }

        public void ShowStats() => _statsContainer.SetActive(true);

        public void HideStats() => _statsContainer.SetActive(false);

        public void UpdateHealth(float percentage) => _healthBar.fillAmount = percentage * _healthBarMultiplier;

        public void UpdateSprint(float percentage) => _sprintBar.fillAmount = percentage * _sprintBarMultiplier;

    }
}