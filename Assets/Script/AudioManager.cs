using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClip;

    public AudioSource clickSource;
    public AudioClip clickClip;

    private bool isMusicOn = true;
    private bool isSoundOn = true;
    private bool isVibratorOn = true;

    public bool IsMusicOn { get { return isMusicOn; } }
    public bool IsSoundOn { get { return isSoundOn; } }
    public bool IsVibratorOn { get { return isVibratorOn; } }

    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
    }

    public void ToggleVibrator()
    {
        isVibratorOn = !isVibratorOn;

        if (isVibratorOn)
        {
            Handheld.Vibrate();
        }
    }

    public void PlayClickSound()
    {
        if (isSoundOn)
        {
            clickSource.PlayOneShot(clickClip);
        }
    }

    private void PlayMusic()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    private void StopMusic()
    {
        musicSource.Stop();
    }
}
