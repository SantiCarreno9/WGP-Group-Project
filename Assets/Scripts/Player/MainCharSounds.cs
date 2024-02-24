using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip[] stepAudio;
    [SerializeField] AudioClip[] hurtAudio;
    [SerializeField] AudioClip[] deathAudio;
    [SerializeField] AudioClip[] jumpAudio;
    [SerializeField] AudioClip[] attackingAudio;

    void Step() {
        audioPlayer.clip = stepAudio[Random.Range(0, stepAudio.Length)];
        audioPlayer.Play();
    }

    void hurt()
    {
        audioPlayer.clip = hurtAudio[Random.Range(0, hurtAudio.Length)];
        audioPlayer.Play();
    }

    void Dead()
    {
        audioPlayer.clip = deathAudio[Random.Range(0, deathAudio.Length)];
        audioPlayer.Play();
    }

    void jump()
    {
        audioPlayer.clip = jumpAudio[Random.Range(0, jumpAudio.Length)];
        audioPlayer.Play();
    }

    void attacking()
    {
        audioPlayer.clip = attackingAudio[Random.Range(0, attackingAudio.Length)];
        audioPlayer.Play();
    }

}
