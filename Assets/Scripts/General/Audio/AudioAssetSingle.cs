using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Asset Single")]
public class AudioAssetSingle : AudioAsset
{    
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private bool loop;
    [Range(0, 1)]
    [SerializeField]
    public float volume = 1.0f;
    [Range(-3, 3)]
    [SerializeField]
    public float pitch = 1.0f;    

    public override void ApplyConfigToAudioSource(AudioSource audioSource)
    {
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }
}
