using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuManager : MonoBehaviour, ISoundPlayer
{

    public AudioSource music;
    public AudioSource soundEffects;

    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVolume();
    }


    public void UpdateVolume()
    {
        music.volume = PlayerPrefs.GetFloat("bgMusicVolume", 1f);
        soundEffects.volume = PlayerPrefs.GetFloat("soundEffectVolume", 1f);
    }

    public void PlaySound(int id)
    {
        soundEffects.PlayOneShot(sounds[id]);
    }
}
