using System;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public Slider musicSlider;
    public Slider soundSlider;

    public TMPro.TMP_Text musicText;
    public TMPro.TMP_Text soundText;


    public AudioClip clip;

    public AudioSource audioSource;

    public void Start()
    {

        float musicVolume = PlayerPrefs.GetFloat("bgMusicVolume", 1f) * 100f;
        float soundVolume = PlayerPrefs.GetFloat("soundEffectVolume", 1f) * 100f;

        musicText.text = musicVolume.ToString() + "%";
        soundText.text = soundVolume.ToString() + "%";

        musicSlider.value = musicVolume;
        soundSlider.value = soundVolume;

    }

    public void SetMusicVolume()
    {
        audioSource.clip = clip;
        audioSource.Play();
        PlayerPrefs.SetFloat("bgMusicVolume", musicSlider.value/100f);
        musicText.text = musicSlider.value.ToString() + "%";
        UpdateVolume();
    }

    public void SetSoundVolume()
    {
        audioSource.clip = clip;
        audioSource.Play();
        PlayerPrefs.SetFloat("soundEffectVolume", soundSlider.value / 100f);
        soundText.text = soundSlider.value.ToString() + "%";
        UpdateVolume();
    }

    void UpdateVolume()
    {
        try
        {
            GameObject.Find("Manager").transform.GetChild(0).GetComponent<AudioManager>().UpdateVolume();
        }
        catch (Exception e)
        {
            print(e.StackTrace);
        }
        try
        {
            GameObject.Find("AudioManager").GetComponent<AudioMenuManager>().UpdateVolume();
        }
        catch(Exception e)
        {
            print(e.StackTrace);
        }
    }


}
