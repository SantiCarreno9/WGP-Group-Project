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
        [SerializeField] private AudioClip[] _stepSounds;
        [SerializeField] private AudioClip[] _jumpSounds;

        [Header("Attack")]
        [SerializeField] private AudioSource _attackAudioSource;
        [SerializeField] private AudioClip[] _attackSounds;
        [Header("Health")]
        [SerializeField] private AudioSource _healthAudioSource;
        [SerializeField] private AudioClip[] _damageSounds;
        [SerializeField] private AudioClip _lowHealthSound;        
        [SerializeField] private AudioClip[] _deadSounds;

        [Space]
        [Header("Modules")]
        [SerializeField] private MovementController _movementModule;
        [SerializeField] private AttackController _attackModule;
        [SerializeField] private HealthModule _healthModule;

        

        private void OnEnable()
        {
            //_healthModule.OnHealthChanged += PlayHealthBasedSound;
            //_healthModule.OnDie += PlayDeadSound;     
        }

        private void OnDisable()
        {
            //_healthModule.OnHealthChanged -= PlayHealthBasedSound;
            //_healthModule.OnDie -= PlayDeadSound;
        }
        // Start is called before the first frame update
        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {
            PlayMovementSounds();
        }

        private void PlayMovementSounds()
        {
            //float velocityMagnitude = _movementModule.GetVelocity().magnitude;
            //float pitch = .5f + velocityMagnitude * pitchMultiplier;
            //_movementAudioSource.pitch = Mathf.Clamp(pitch, 0.1f, 3f);
        }

        #region MOVEMENT

        public void PlayStepSound()
        {
            _movementAudioSource.clip = _stepSounds[Random.Range(0, _stepSounds.Length)];
            _movementAudioSource.Play();
        }

        public void PlayJumpSound()
        {
            _movementAudioSource.clip = _jumpSounds[Random.Range(0, _jumpSounds.Length)];
            _movementAudioSource.Play();
        }

        #endregion

        #region ATTACK

        public void PlayAttackSound()
        {
            _attackAudioSource.clip = _jumpSounds[Random.Range(0, _jumpSounds.Length)];
            _attackAudioSource.Play();
        }


        #endregion

        public void PlayDamageSound()
        {
            _healthAudioSource.clip = _damageSounds[Random.Range(0, _stepSounds.Length)];
            _healthAudioSource.Play();
        }

        private void PlayHealthBasedSound(int healthPoints)
        {
            float percentage = (float)healthPoints / (float)_healthModule.GetMaxHealth();
            if (percentage < 0.25f)
            {
                if (!_healthAudioSource.isPlaying)
                {
                    //_healthAudioSource.clip = _lowHealthSound;
                    _healthAudioSource.PlayOneShot(_lowHealthSound);
                    //_healthAudioSource.loop = true;
                }
            }
            else
            {
                _healthAudioSource.Stop();
                //_healthAudioSource.loop = false;
            }
        }

        public void PlayDeadSound()
        {
            _healthAudioSource.clip = _deadSounds[Random.Range(0, _deadSounds.Length)];
            _healthAudioSource.Play();
        }

        public void Mute()
        {
            _movementAudioSource.mute = true;
            _attackAudioSource.mute = true;
            _healthAudioSource.mute= true;
        }

        public void Unmute()
        {
            _movementAudioSource.mute = false;
            _attackAudioSource.mute = false;
            _healthAudioSource.mute = false;
        }
    }
}