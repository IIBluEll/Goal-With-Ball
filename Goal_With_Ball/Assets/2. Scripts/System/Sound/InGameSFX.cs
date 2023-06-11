using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class InGameSFX : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] collSounds;
    [SerializeField] private AudioClip powerSlide;
    [SerializeField] private AudioClip pauseBtn;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BallCollSFX()
    {
        var randNum = Random.Range(0, collSounds.Length);

        audioSource.clip = collSounds[randNum];
        audioSource.Play();
    }

    public void PowerGauageSFX()
    {
        audioSource.clip = powerSlide;
        audioSource.Play();
    }

    public void PauseBtnSFX()
    {
        audioSource.clip = pauseBtn;
        audioSource.Play();
    }
}
