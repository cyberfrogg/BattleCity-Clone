using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioSource BGM;
    public AudioSource SFX;
    public AudioSource BackGroundSFX;




    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    


    public void PlayBGM(AudioClip bgmAudioClip)
    {
        BGM.clip = bgmAudioClip;
        BGM.loop = false;
        BGM.Play();

    }

    public void PlaySFX(AudioClip sfxAudioClip)
    {
        //StopSFX();
        SFX.clip = sfxAudioClip;
        SFX.loop = false;
        SFX.Play();
    }


    public void PlayBackGroundSFX(AudioClip sfxAudioClip)
    {
        BackGroundSFX.Stop();
        BackGroundSFX.clip = sfxAudioClip;
        BackGroundSFX.loop = true;
        BackGroundSFX.Play();
    }

    public void StopSFX()
    {
        SFX.Stop();
    }

    public void StopAll()
    {
        BGM.Stop();
        SFX.Stop();
        BackGroundSFX.Stop();
    }
}
