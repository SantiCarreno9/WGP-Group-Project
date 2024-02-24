using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Audio/Audio Asset Group")]
public class AudioAssetGroup : AudioAsset
{
    [SerializeField] List<AudioAssetSingle> audioAssets;

    public override void ApplyConfigToAudioSource(AudioSource audioSource)
    {
        audioAssets[Random.Range(0, audioAssets.Count)].ApplyConfigToAudioSource(audioSource);
    }
}
