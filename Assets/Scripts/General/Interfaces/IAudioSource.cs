using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioSourceType
{
    Music,
    SFX
}

public interface IAudioSource
{
    AudioSourceType Type { get; }
    public void Mute();
    public void Unmute();
}
