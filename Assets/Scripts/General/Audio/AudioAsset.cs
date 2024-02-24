using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Asset")]
public class AudioAsset : ScriptableObject
{
    public string AudioName;
    public AudioCategory Category;
    public AudioClip Clip;
    public bool Loop = false;
    [Range(0,1)]
    public float Volume = 1.0f;
    [Range(0, 1)]
    public float Pitch = 1.0f;

    public static void SetConfig(AudioAsset asset, AudioSource audioSource)
    {
        audioSource.clip = asset.Clip;
        audioSource.volume = asset.Volume;
        audioSource.pitch = asset.Pitch;
        audioSource.loop = asset.Loop;
    }
}
