using UnityEngine;
using UnityEngine.UI;

namespace Character.UI
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private GameObject _statsContainer;
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _sprintBar;
        [SerializeField]private float _healthBarMultiplier = 0.75f;
        [SerializeField] private float _sprintBarMultiplier = 0.25f;        

        public void ShowStats() => _statsContainer.SetActive(true);

        public void HideStats() => _statsContainer.SetActive(false);

        public void UpdateHealth(float percentage) => _healthBar.fillAmount = percentage * _healthBarMultiplier;

        public void UpdateSprint(float percentage) => _sprintBar.fillAmount = percentage * _sprintBarMultiplier;

    }
}