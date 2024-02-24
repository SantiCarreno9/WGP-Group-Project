using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Asset Single")]
public class AudioAssetSingle : AudioAsset
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool loop;
    [Range(0, 1)]
    [SerializeField]
    public float volume = 1.0f;
    [Range(0, 1)]
    [SerializeField]
    public float pitch = 1.0f;

    public override void ApplyConfigToAudioSource(AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }
}
