using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    public AudioSource main;
    public AudioSource background;
    public AudioSource effects;
    public AudioSource wheelSource;

    [Header("Clips")]
    public AudioClip mainTheme;
    public AudioClip menuTheme;
    public AudioClip scoreTheme;
    public AudioClip wheel;

    [Header("FX")]
    public AudioClip captureDog;
    public AudioClip rightDog;
    public AudioClip wrongDog;
    public AudioClip combo;
    public AudioClip timeUp;
    public AudioClip timer5seconds;
    public AudioClip timer30seconds;
    public AudioClip confirmButton;
    public AudioClip selectButton;
    public AudioClip backButton;
    public AudioClip comboBreak;
    public AudioClip steps;
    public AudioClip bark1;
    public AudioClip bark2;
    public AudioClip bark3;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        main = GetComponents<AudioSource>()[0];
        background = GetComponents<AudioSource>()[1];
        effects = GetComponents<AudioSource>()[2];
        wheelSource = GetComponents<AudioSource>()[3];

        wheelSource.clip = wheel;
    }

    public void PlayGameTheme()
    {
        main.clip = mainTheme;
        main.Play();
    }

    public void PlayTimeUp()
    {
        effects.PlayOneShot(timeUp);
    }

    public void PlayTimer5Seconds()
    {
        effects.PlayOneShot(timer5seconds);
    }

    public void PlayTimer30Seconds()
    {
        effects.PlayOneShot(timer30seconds);
    }

    public void PlayCaptureDog()
    {
        effects.PlayOneShot(captureDog);
    }

    public void PlayRightDog()
    {
        effects.PlayOneShot(rightDog);
    }

    public void PlayWrongDog()
    {
        effects.PlayOneShot(wrongDog);
    }

    public void PlayCombo()
    {
        effects.PlayOneShot(combo);
    }

    public void PlayConfirmButton()
    {
        effects.PlayOneShot(confirmButton);
    }

    public void PlaySelectButton()
    {
        effects.PlayOneShot(selectButton);
    }

    public void PlayBackButton()
    {
        effects.PlayOneShot(backButton);
    }

    public void PlayComboBreak()
    {
        effects.PlayOneShot(comboBreak);
    }

    public void PlayMenuTheme()
    {
        main.clip = menuTheme;
        main.Play();
    }

    public void PlayScoreTheme()
    {
        background.clip = scoreTheme;
        background.Play();
    }

    public void PlayWheel()
    {
        wheelSource.UnPause();
    }

    public void StopWheel()
    {
        wheelSource.Pause();
    }
}
