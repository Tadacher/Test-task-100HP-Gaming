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
        _asteroidDeathSoundHandlers += PlayAsteroidDeathSound;
        _upgradeBoughtSoundHandlers += PlayBuySound;
        MissileLaunchedSoundHandlers += PlayMissileLaunchSound;
        _themeSource.clip = _audioSetup._theme;
        _themeSource.Play();
    }

    internal OnAsteroidDeath _asteroidDeathSoundHandlers;
    internal OnUpgradeBought _upgradeBoughtSoundHandlers;
    internal OnMissileLaunched MissileLaunchedSoundHandlers;

    void PlayAsteroidDeathSound() => 
        _audioSource.PlayOneShot(_audioSetup._asteroidDeathSounds[Random.Range(0, _audioSetup._asteroidDeathSounds.Length - 1)], 1f);
    void PlayBuySound() => 
        _audioSource.PlayOneShot(_audioSetup._buy, 1f);
    void PlayMissileLaunchSound() => 
        _audioSource.PlayOneShot(_audioSetup._missileLaunch, 1f);

}
