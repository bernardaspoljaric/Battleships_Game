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

    [SerializeField] private AudioSource[] gameMusic;
    [SerializeField] private AudioSource[] soundEffects;


    [SerializeField] private UIManager UIManager;

    private void Awake()
    {
        UIManager.musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        UIManager.effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume");
    }

    private void Start()
    {
        // Set music volume
        for (int i = 0; i < gameMusic.Length; i++)
        {
            gameMusic[i].volume = PlayerPrefs.GetFloat("musicVolume");
        }

        // Set effects volume
        for (int i = 0; i < soundEffects.Length; i++)
        {
            soundEffects[i].volume = PlayerPrefs.GetFloat("effectsVolume");
        }
    }
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

    public void ChangeMusicVolume()
    {
        for (int i = 0; i < gameMusic.Length; i++)
        {
           gameMusic[i].volume = UIManager.musicSlider.value;
        }

        SaveMusicVolume(UIManager.musicSlider.value);
    }
    public void ChangeEffectsVolume()
    {
        for (int i = 0; i < soundEffects.Length; i++)
        {
            soundEffects[i].volume = UIManager.effectsSlider.value;
        }
        SaveEffectsVolume(UIManager.effectsSlider.value);
    }

    private void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    private void SaveEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat("effectsVolume", volume);
    }
}
