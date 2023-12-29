using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource audioSource;
    public bool sound = true;

    private void Awake()
    {
        MakeSingleton();
        audioSource = GetComponent<AudioSource>();
    }
    public void SoundOff()
    {
        sound=!sound;
    }    
    public void PlaySoundFX(AudioClip clip,float volume)
    {
        if (sound)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }    
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
