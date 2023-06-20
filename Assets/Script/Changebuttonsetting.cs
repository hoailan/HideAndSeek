using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changebuttonsetting : MonoBehaviour
{
    public Image musicOnImage;
    public Image musicOffImage;
    public Image soundOnImage;
    public Image soundOffImage;
    public Image vibratorOnImage;
    public Image vibratorOffImage;
    public AudioClip clickSound;

    private AudioManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<AudioManager>();
    }

    public void ToggleMusic()
    {
        soundManager.ToggleMusic();

        if (soundManager.IsMusicOn)
        {
            musicOnImage.gameObject.SetActive(true);
            musicOffImage.gameObject.SetActive(false);
        }
        else
        {
            musicOnImage.gameObject.SetActive(false);
            musicOffImage.gameObject.SetActive(true);
        }
    }

    public void ToggleSound()
    {
        soundManager.ToggleSound();

        if (soundManager.IsSoundOn)
        {
            soundOnImage.gameObject.SetActive(true);
            soundOffImage.gameObject.SetActive(false);
        }
        else
        {
            soundOnImage.gameObject.SetActive(false);
            soundOffImage.gameObject.SetActive(true);
        }

        if (soundManager.IsSoundOn)
        {
            PlayClickSound();
        }
        else
        {
            StopClickSound();
        }
    }

    public void ToggleVibrator()
    {
        soundManager.ToggleVibrator();

        if (soundManager.IsVibratorOn)
        {
            vibratorOnImage.gameObject.SetActive(true);
            vibratorOffImage.gameObject.SetActive(false);
        }
        else
        {
            vibratorOnImage.gameObject.SetActive(false);
            vibratorOffImage.gameObject.SetActive(true);
        }
    }

    private void PlayClickSound()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
    }

    private void StopClickSound()
    {

    }
}
