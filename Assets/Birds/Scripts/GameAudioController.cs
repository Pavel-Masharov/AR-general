using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    public static GameAudioController instance;

    [SerializeField] private AudioClip _clickButton;


    private AudioSource _audioSourceMusic;
    private AudioSource _audioSourceSounds;
    private AudioSource _audioSourceSoundsUI;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);


        _audioSourceMusic = gameObject.AddComponent<AudioSource>();
        _audioSourceSounds = gameObject.AddComponent<AudioSource>();
        _audioSourceSoundsUI = gameObject.AddComponent<AudioSource>();
    }

    public void PlayOneShotSound(AudioClip audioClip)
    {
        _audioSourceSounds.PlayOneShot(audioClip);
    }
    public void PlaySound(AudioClip audioClip, bool loop)
    {
        _audioSourceSounds.clip = audioClip;
        _audioSourceSounds.loop = loop;
        _audioSourceSounds.Play();
    }


    public void StopAllSunds()
    {
        _audioSourceMusic.Stop();
        _audioSourceSounds.Stop();
        _audioSourceSoundsUI.Stop();
    }

    public void DisableAllSounds()
    {
        _audioSourceMusic.volume = 0;
        _audioSourceSounds.volume = 0;
        _audioSourceSoundsUI.volume = 0;
    }

    public void EnableAllSounds()
    {
        _audioSourceMusic.volume = 1;
        _audioSourceSounds.volume = 1;
        _audioSourceSoundsUI.volume = 1;
    }


    public void PlayClickButton()
    {
        _audioSourceSoundsUI.PlayOneShot(_clickButton);
    }
}
