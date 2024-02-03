using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip stepAudio;

    void playStep() { audioPlayer.clip = stepAudio;
        audioPlayer.Play();
    }

}
