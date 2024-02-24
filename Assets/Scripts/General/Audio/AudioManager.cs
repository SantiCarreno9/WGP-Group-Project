using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] private List<AudioAsset> _audioAssets;

    public AudioAsset GetSFX(string name)
    {
        AudioAsset asset = null;
        asset = _audioAssets.Find(x => x.name == name);
        return asset;
    }

    public void PlaySFX(string name, AudioSource audioSource)
    {
        AudioAsset asset = null;
        asset = GetSFX(name);
        if (asset != null)
        {
            AudioAsset.SetConfig(asset, audioSource);
            audioSource.Play();
        }
    }
}
