using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(AudioSource))]
public class UI_SFX : MonoBehaviour
{
   private AudioSource sfxAudioSource;
   [SerializeField] private List<AudioClip> btn_SfxClips;

   [SerializeField] private AudioClip playBtn_SFX;
   

   private void Awake()
   {
      sfxAudioSource = GetComponent<AudioSource>();
   }

   public void UI_Btn_SFX()
   {
      var randNum = Random.Range(0, btn_SfxClips.Count);

      sfxAudioSource.clip = btn_SfxClips[randNum];
      sfxAudioSource.Play();
   }

   public void PlayBtn_SFX()
   {
      sfxAudioSource.clip = playBtn_SFX;
      sfxAudioSource.Play();
   }
}
