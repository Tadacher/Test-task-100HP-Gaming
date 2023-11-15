using UnityEngine;
using Events;

public class AudioService
{
   
    AudioSource _audioSource;
    AudioSource _themeSource;

    private AudioSetup _audioSetup;

    public AudioService(AudioSetup audioSetup, AudioSource audioSource)
    {
        _audioSource = audioSource;
        _themeSource = audioSource;

        _audioSetup = audioSetup;
        _themeSource.clip = _audioSetup._theme;
        _themeSource.Play();
    }

    public void PlayOneShot(AudioClip clip) => 
        _audioSource.PlayOneShot(clip);
    public void PlayAsteroidDeathSound() => 
        _audioSource.PlayOneShot(_audioSetup._asteroidDeathSounds[Random.Range(0, _audioSetup._asteroidDeathSounds.Length - 1)], 1f);
    public void PlayBuySound() => 
        _audioSource.PlayOneShot(_audioSetup._buy, 1f);
    public void PlayMissileLaunchSound() => 
        _audioSource.PlayOneShot(_audioSetup._missileLaunch, 1f);

}
