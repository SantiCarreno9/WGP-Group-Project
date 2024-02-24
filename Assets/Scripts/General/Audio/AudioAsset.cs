using UnityEngine;

public abstract class AudioAsset : ScriptableObject
{
    public abstract void ApplyConfigToAudioSource(AudioSource audioSource);
}
