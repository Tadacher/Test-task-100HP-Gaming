using UnityEngine;
[CreateAssetMenu(fileName = "AudioSetup", menuName = "ScriptableObjects/AudioSetup")]
public class AudioSetup :ScriptableObject
{
    public AudioClip[] _asteroidDeathSounds;
    public AudioClip _missileLaunch;
    public AudioClip _buy;
    public AudioClip _menuInteraction;
    public AudioClip _theme;
}