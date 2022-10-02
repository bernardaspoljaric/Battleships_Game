using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickAudioSource;
    [SerializeField] private AudioSource hitAudioSource;
    [SerializeField] private AudioSource missAudioSource;
    [SerializeField] private AudioSource gameThemeAudioSource;
    [SerializeField] private AudioSource battleAudioSource;

    public void PlayClickAudio()
    {
        buttonClickAudioSource.Play();
    }

    public void PlayHitAudio()
    {
        hitAudioSource.Play();
    }

    public void PlayMissAudio()
    {
        missAudioSource.Play();
    }

    public void PlayThemeAudio()
    {
        battleAudioSource.Stop();
        gameThemeAudioSource.UnPause();
    }

    public void PlayBattleAudio()
    {
        gameThemeAudioSource.Pause();
        battleAudioSource.Play();
    }
}
