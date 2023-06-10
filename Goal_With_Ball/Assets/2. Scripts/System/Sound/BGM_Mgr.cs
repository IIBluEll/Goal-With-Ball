using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 1. 각 씬에 맞는 배경음악 지정
/// 2. 씬이 변경될 때마다 그에 맞는 배경음악 전환
/// </summary>
public class BGM_Mgr : MonoBehaviour
{
    public static BGM_Mgr instance;

    [SerializeField] private AudioSource backGroundMusic;

    [SerializeField] private AudioClip title_music;
    [SerializeField] private AudioClip inGame_music;
    
    private Dictionary<string, AudioClip> bgms;

    private float time = .01f; // 음악 전환 시간

    private void OnEnable()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoad;

        bgms = new Dictionary<string, AudioClip>()
        {
            { "MainMenu", title_music },
            { "InGame", inGame_music },
        };
    }


    // 씬이 로드 될 때 자동으로 호출됨
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        PlayBGM();
    }

    // 현재 씬 이름을 가져와, Dictionary에 저장된 값과 대조
    // 음악을 바꾸는 코루틴 실행
    private void PlayBGM()
    {
        var nowSceneName = SceneManager.GetActiveScene().name;


        if (bgms != null && bgms.ContainsKey(nowSceneName))
        {
            StartCoroutine(ChangeBGM(bgms[nowSceneName]));
        }
        else
        {
            Debug.LogWarning("현재 씬에 맞는 배경음악이 없습니다.");
        }
    }

    public void BgmToggle(bool isOn)
    {
        backGroundMusic.volume = isOn ? 1 : 0;
    }

    public void ChangeBgm(AudioClip newclip)
    {
        StartCoroutine(ChangeBGM(newclip));
    }
    
    IEnumerator ChangeBGM(AudioClip newclip)
    {
        if (backGroundMusic.clip == newclip)
            yield break;
        //--- 볼륨 낮추기 --- // 

        var volume = backGroundMusic.volume;
        var ntime = 0f;

        while (ntime < time)
        {
            ntime += Time.deltaTime;
            backGroundMusic.volume = Mathf.Lerp(volume, 0, ntime / time);
            yield return null;
        }

        // --- 노래 바꾸기 --- //

        backGroundMusic.clip = newclip;
        backGroundMusic.volume = 0;
        backGroundMusic.Play();

        // --- 볼륨 올리기 --- // 

        ntime = 0;
        while (ntime < time)
        {
            ntime += Time.deltaTime;
            backGroundMusic.volume = Mathf.Lerp(0, volume, ntime / time);
            yield return null;
        }
    }
}