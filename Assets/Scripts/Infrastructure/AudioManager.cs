using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] asteroidDeayhSoundsSounds;
    [SerializeField] AudioClip missileLaunch;
    [SerializeField] AudioClip buy;
    [SerializeField] AudioClip menuInteraction;
    [SerializeField] AudioClip theme;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource themeSource;

    internal AsteroidDeath asteroidDeathSoundHandlers;
    internal UpgradeBought upgradeBoughtSoundHandlers;
    internal MissileLaunched missileLaunchedSoundHandlers;

    internal void Initialize()
    {
        asteroidDeathSoundHandlers += PlayAsteroidDeathSound;
        upgradeBoughtSoundHandlers += PlayBuySound;
        missileLaunchedSoundHandlers += PlayMissileLaunchSound;
        themeSource.clip = theme;
        themeSource.Play();
         
    }

    void PlayAsteroidDeathSound()
    {
        audioSource.PlayOneShot(asteroidDeayhSoundsSounds[Random.Range(0, asteroidDeayhSoundsSounds.Length - 1)],1f);
    }
    void PlayBuySound()
    {
        audioSource.PlayOneShot(buy, 1f);
    }
    void PlayMissileLaunchSound()
    {
        audioSource.PlayOneShot(missileLaunch,1f);
    }

}
