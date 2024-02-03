using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip scratch;
    [SerializeField] AudioClip splat;

    void Scratch()
    {
        audioPlayer.clip = scratch;
        audioPlayer.Play();
    }
    void Splat()
    {
        audioPlayer.clip = splat;
        audioPlayer.Play();
    }
}
