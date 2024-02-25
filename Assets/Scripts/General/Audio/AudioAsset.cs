using UnityEngine;

public abstract class AudioAsset : ScriptableObject
{
    public string Name;
    public abstract void ApplyConfigToAudioSource(AudioSource audioSource);
}
