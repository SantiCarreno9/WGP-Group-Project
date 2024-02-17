/* HealthModule.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script handles the health system for the main character
 * 
 */
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class HealthModule : MonoBehaviour
    {
        [SerializeField] private int _maxHealthPoints = 200;
        [SerializeField] private float _damageTimeout = 0.2f;
        public int HealthPoints { get; private set; }
        private float _recoveryTime = 0f;

        public UnityAction OnDamageReceived;
        public UnityAction OnHealthRestored;
        public UnityAction<int> OnHealthChanged;
        public UnityAction OnDie;

        private void Awake()
        {
            HealthPoints = _maxHealthPoints;
        }
       
        public void Restore(int points)
        {
            HealthPoints = points;
            OnHealthChanged?.Invoke(points);
            OnHealthRestored?.Invoke();
        }

        /// <summary>
        /// Reduces the character's health, updates the recovery time
        /// and triggers some events
        /// </summary>
        /// <param name="points"></param>
        public void Damage(int points)
        {
            _recoveryTime = Time.time + _damageTimeout;
            if (HealthPoints > 0)
            {
                HealthPoints -= points;
                OnHealthChanged?.Invoke(HealthPoints);
                OnDamageReceived?.Invoke();

                if (HealthPoints <= 0)
                    Die();
            }
        }

        public bool IsReceivingDamage() => Time.time < _recoveryTime;

        /// <summary>
        /// Plays the dead animation and disables the controls
        /// </summary>
        public void Die()
        {
            OnDie?.Invoke();
        }

        public int GetMaxHealth() => _maxHealthPoints;
        public bool HasMaxHealth() => (HealthPoints / GetMaxHealth()) == 1;
    }
}