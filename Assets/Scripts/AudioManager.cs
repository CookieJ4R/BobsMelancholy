using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, ISoundPlayer
{

    public AudioSource soundEffectSource;
    public AudioSource bgMusicSource;

    public AudioClip dashSound;
    public AudioClip deathSound;
    public AudioClip goalSound;
    public AudioClip jumpSound;
    public AudioClip walljumpSound;
    public AudioClip landingSound;

    public AudioClip[] menuSounds;

    private void Start()
    {
        UpdateVolume();
    }

    public void PlayDashSound()
    {
        soundEffectSource.PlayOneShot(dashSound);
    }
    public void PlayDeathSound()
    {
        soundEffectSource.PlayOneShot(deathSound);
    }

    public void PlayGoalSound()
    {
        soundEffectSource.PlayOneShot(goalSound);
    }

    public void PlayJumpSound()
    {
        soundEffectSource.PlayOneShot(jumpSound);
    }

    public void PlayWallJumpSound()
    {
        soundEffectSource.PlayOneShot(walljumpSound);
    }

    public void PlayLandingSound()
    {
        soundEffectSource.PlayOneShot(landingSound);
    }

    public void UpdateVolume()
    {
        bgMusicSource.volume = PlayerPrefs.GetFloat("bgMusicVolume", 1f);
        soundEffectSource.volume = PlayerPrefs.GetFloat("soundEffectVolume", 1f);
    }

    public void PlaySound(int id)
    {
        soundEffectSource.PlayOneShot(menuSounds[id]);
    }
}
