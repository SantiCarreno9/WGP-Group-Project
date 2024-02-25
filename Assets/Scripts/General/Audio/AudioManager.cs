using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] private List<AudioAsset> _audioAssets;

    private AudioAsset GetAsset(string name)
    {
        AudioAsset asset = null;
        asset = _audioAssets.Find(x => x.Name == name);
        return asset;
    }

    public void PlayAsset(string name, AudioSource audioSource)
    {
        var asset = GetAsset(name);
        if (asset != null)
        {
            asset.ApplyConfigToAudioSource(audioSource);
            audioSource.Play();
        }
    }
}
