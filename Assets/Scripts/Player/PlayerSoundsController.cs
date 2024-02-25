using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class PlayerSoundsController : MonoBehaviour, IAudioSource
    {
        public AudioSourceType Type => AudioSourceType.SFX;
        [Header("Audios")]
        [Header("Movement")]
        [SerializeField] private AudioSource _movementAudioSource;
        [SerializeField] private string _stepSoundsName;
        [SerializeField] private string _jumpSoundsName;

        [Header("Attack")]
        [SerializeField] private AudioSource _attackAudioSource;
        [SerializeField] private string _attackSoundsName;
        [Header("Health")]
        [SerializeField] private AudioSource _healthAudioSource;
        [SerializeField] private string _damageSoundsName;
        [SerializeField] private string _deathSoundsName;

        [Space]
        [Header("Modules")]
        [SerializeField] private HealthModule _healthModule;



        private void OnEnable()
        {
            //_healthModule.OnHealthChanged += PlayHealthBasedSound;            
        }

        private void OnDisable()
        {
            //_healthModule.OnHealthChanged -= PlayHealthBasedSound;            
        }

        #region MOVEMENT

        public void PlayStepSound()
        {
            AudioManager.Instance.PlayAsset(_stepSoundsName, _movementAudioSource);
        }

        public void PlayJumpSound()
        {
            AudioManager.Instance.PlayAsset(_jumpSoundsName, _movementAudioSource);
        }

        #endregion

        #region ATTACK

        public void PlayAttackSound()
        {
            AudioManager.Instance.PlayAsset(_attackSoundsName, _attackAudioSource);
        }


        #endregion

        public void PlayDamageSound()
        {
            AudioManager.Instance.PlayAsset(_damageSoundsName, _attackAudioSource);
        }

        //private void PlayHealthBasedSound(int healthPoints)
        //{
        //    float percentage = (float)healthPoints / (float)_healthModule.GetMaxHealth();
        //    if (percentage < 0.25f)
        //    {
        //        if (!_healthAudioSource.isPlaying)
        //        {
        //            //_healthAudioSource.clip = _lowHealthSound;
        //            _healthAudioSource.PlayOneShot(_lowHealthSound);
        //            //_healthAudioSource.loop = true;
        //        }
        //    }
        //    else
        //    {
        //        _healthAudioSource.Stop();
        //        //_healthAudioSource.loop = false;
        //    }
        //}

        public void PlayDeadSound()
        {
            AudioManager.Instance.PlayAsset(_deathSoundsName, _attackAudioSource);
        }

        public void Mute()
        {
            _movementAudioSource.mute = true;
            _attackAudioSource.mute = true;
            _healthAudioSource.mute = true;
        }

        public void Unmute()
        {
            _movementAudioSource.mute = false;
            _attackAudioSource.mute = false;
            _healthAudioSource.mute = false;
        }
    }
}